using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class Point3D
{
    public int X;
    public int Y;
    public int Z;

    public void New(int X, int Y, int Z)
    {
        Me.X = X;
        Me.Y = Y;
        Me.Z = Z;
    }
    public void New()
    {
    }

    Public Shadows ReadOnly Property ToString() As String
        {
            get
            {
            return String.Format("X:{0}, Y:{1}, Z:{2}", X, Y, Z);
        }
    }
}



public class MapNode
{
    Implements ICloneable;
    Implements IComparable(Of MapNode);

    public string Key;
    public string Text;
    public New Point3D Location;
    public int Height = 4;
    public int Width = 6;
    public int Page;
    public bool Pinned = false;
    'Public NodeImage As Bitmap
    internal New Generic.Dictionary<DirectionsEnum, Anchor> Anchors;
    internal New Generic.Dictionary<DirectionsEnum, MapLink> Links;
    private bool bOverlapping = false;
    private bool bSeen = false;
    Friend eInEdge, eOutEdge As DirectionsEnum;
    Friend ptIn, ptOut As Point;
    Friend bHasUp, bHasDown, bHasIn, bHasOut As Boolean;
    Friend bDrawIn, bDrawOut As Boolean;

    public bool Overlapping { get; set; }
        {
            get
            {
            return bOverlapping;
        }
set(ByVal Boolean)
            if (value <> bOverlapping)
            {
                bOverlapping = value
                if (Adventure.Map IsNot null)
                {
                    For Each p As MapPage In Adventure.Map.Pages.Values
                        For Each n As MapNode In p.Nodes
                            if (n.Overlapping)
                            {
                                Adventure.Map.FirstOverlapPage = p.iKey;
                                Exit Property;
                            }
                        Next;
                    Next;
                }
                Adventure.Map.FirstOverlapPage = -1;
            }
        }
    }


    public bool Seen { get; set; }
        {
            get
            {
#if Generator
            return true;
#else
            return bSeen;
#endif
        }
set(ByVal Boolean)
            if (value <> bSeen)
            {
                bSeen = value
                if (bSeen)
                {
                    Adventure.Map.Pages(Page).Seen = true;
                Else
                    Adventure.Map.Pages(Page).RecalculateSeen();
                }
            }
        }
    }

    ' The translated points
    Public Points() As Point = {New Point(0, 0), New Point(0, 0), New Point(0, 0), New Point(0, 0)}
    public Point ptUp;
    public Point ptDown;

    private Object Implements System.ICloneable.Clone Clone()
    {
        return Me.MemberwiseClone;
    }
    public MapNode CloneMe()
    {
        return CType(Clone(), MapNode);
    }

    public Integer Implements System.IComparable<MapNode> CompareTo(MapNode other)
    {
        'Return other.Z.CompareTo(Me.Z)
        If Location.Z <> other.Location.Z Then Return other.Location.Z.CompareTo(Location.Z)
        If Location.Y <> other.Location.Y Then Return Location.Y.CompareTo(other.Location.Y) ' other.Y.CompareTo(Me.Y)
        return Location.X.CompareTo(other.Location.X);
    }

    public void New(bool bCreateAnchors = true)
    {

        if (bCreateAnchors)
        {
            For Each d As DirectionsEnum In New DirectionsEnum() {DirectionsEnum.NorthWest, DirectionsEnum.North, DirectionsEnum.NorthEast, DirectionsEnum.East, DirectionsEnum.SouthEast, DirectionsEnum.South, DirectionsEnum.SouthWest, DirectionsEnum.West}
                private New Anchor anchor;
                anchor.Direction = d;
                anchor.Parent = Me;
                Anchors.Add(d, anchor);
            Next;
        }

    }

}



public class MapLink
{
    public Drawing2D.DashStyle Style;
    public bool Duplex;
    public string sSource;
    internal DirectionsEnum eSourceLinkPoint;
    public string sDestination;
    internal DirectionsEnum eDestinationLinkPoint;
    Public OrigMidPoints() As Point3D = {} ' In case user wishes to enhance link
    Public Points() As Point
    public Point ptStartB;
    public Point ptEndB;
    internal New Generic.List<Anchor> Anchors;
}


public class Anchor
{
    Public Points() As Point = {New Point(0, 0), New Point(0, 0), New Point(0, 0), New Point(0, 0)}
    'Public MouseCursor As Cursor
    internal DirectionsEnum Direction;
    public object Parent;
    public bool bVisible;

    public bool Visible { get; set; }
        {
            get
            {
            return bVisible;
        }
set(ByVal Boolean)
            if (value <> bVisible)
            {
                bVisible = value
            }
        }
    }
    public bool HasLink = false;

    public Point3D GetApproxPoint3D { get; }
        {
            get
            {
            if (TypeOf Parent Is MapNode)
            {
                private MapNode nodParent = CType(Parent, MapNode);
                With nodParent.Location;
                    switch (Direction)
                    {
                        case DirectionsEnum.NorthWest:
                            {
                            return nodParent.Location;
                        case DirectionsEnum.North:
                            {
                            return new Point3D(.X + CInt(nodParent.Width / 2), .Y, .Z);
                        case DirectionsEnum.NorthEast:
                            {
                            return new Point3D(.X + nodParent.Width, .Y, .Z);
                        case DirectionsEnum.East:
                            {
                            return new Point3D(.X + nodParent.Width, .Y + CInt(nodParent.Height / 2), .Z);
                        case DirectionsEnum.SouthEast:
                            {
                            return new Point3D(.X + nodParent.Width, .Y + nodParent.Height, .Z);
                        case DirectionsEnum.South:
                            {
                            return new Point3D(.X + CInt(nodParent.Width / 2), .Y + nodParent.Height, .Z);
                        case DirectionsEnum.SouthWest:
                            {
                            return new Point3D(.X, .Y + nodParent.Height, .Z);
                        case DirectionsEnum.West:
                            {
                            return new Point3D(.X, .Y + CInt(nodParent.Height / 2), .Z);
                        default:
                            {
                            return new Point3D(0, 0, 0);
                    }
                }
            Else
                return new Point3D(0, 0, 0);
            }
        }
    }

}


public class MapPage
{

    public int iKey;

    private bool bSeen;
    public bool Seen { get; set; }
        {
            get
            {
            return bSeen;
        }
set(ByVal Boolean)
            if (value <> bSeen)
            {
                bSeen = value
                try
                {
#if Runner
#if Mono
                If UserSession.Map.tabsMap.TabPages.ContainsKey(iKey.ToString) Then UserSession.Map.tabsMap.TabPages(iKey.ToString).Visible = bSeen
#elif Not www
                    If UserSession.Map.tabsMap.Tabs.Exists(iKey.ToString) Then UserSession.Map.tabsMap.Tabs(iKey.ToString).Visible = bSeen
#endif
#endif
                Catch
                }
            }
        }
    }


    internal void RecalculateSeen()
    {
        private bool lbSeen = false;
        For Each n As MapNode In Nodes
            if (n.Seen)
            {
                lbSeen = True
                Exit For;
            }
        Next;
        Seen = lbSeen
    }


    public void New(int iKey)
    {
        Me.iKey = iKey;
        Label = "Page " & iKey + 1
    }


    public string Label;
    public New Generic.List<MapNode> Nodes;


    public MapNode GetNode(string sKey)
    {
        For Each node As MapNode In Nodes
            If node.Key = sKey Then Return node
        Next;
        return null;
    }


    public void AddNode(MapNode node)
    {
        Nodes.Add(node);
    }


    public void RemoveNode(MapNode node)
    {
        Nodes.Remove(node);
    }


    public void SortNodes()
    {
        Nodes.Sort();
    }


    ''' <summary>
    ''' Checks to see if any nodes overlap
    ''' </summary>
    ''' <returns>True, if any overlap</returns>
    ''' <remarks></remarks>
    public bool CheckForOverlaps()
    {

        For Each node As MapNode In Nodes
            node.Overlapping = false;
        Next;

        for (int i = 0; i <= Nodes.Count - 1; i++)
        {
            for (int j = i + 1; j <= Nodes.Count - 1; j++)
            {
                if (Nodes(i).Location.Z = Nodes(j).Location.Z)
                {
                    private int AX1 = Nodes(i).Location.X;
                    private int AX2 = Nodes(i).Location.X + Nodes(i).Width;
                    private int AY1 = Nodes(i).Location.Y;
                    private int AY2 = Nodes(i).Location.Y + Nodes(i).Height;
                    private int BX1 = Nodes(j).Location.X;
                    private int BX2 = Nodes(j).Location.X + Nodes(j).Width;
                    private int BY1 = Nodes(j).Location.Y;
                    private int BY2 = Nodes(j).Location.Y + Nodes(j).Height;

                    if (AX1 < BX2 && AX2 > BX1 && AY1 < BY2 && AY2 > BY1)
                    {
                        Nodes(i).Overlapping = true;
                        Nodes(j).Overlapping = true;
                        CheckForOverlaps = True
                    }
                }
            Next;
        Next;

    }

}


public class clsMap
{

    public New Generic.Dictionary<int, MapPage> Pages;
    public string SelectedPage;
    private int iFirstOverlapPage = -1;

    public int FirstOverlapPage { get; set; }
        {
            get
            {
            return iFirstOverlapPage;
        }
set(ByVal Integer)
            if (iFirstOverlapPage <> value)
            {
#if Generator
                iFirstOverlapPage = value
                With fGenerator.UTMMain.Tools("MapWarning");
                    if (iFirstOverlapPage = -1)
                    {
                        .SharedProps.Visible = false;
                    Else
                        .SharedProps.Visible = true;
                        .Tag = iFirstOverlapPage;
                    }
                }
#endif
            }
        }
    }

    public void RecalculateLayout()
    {

        Pages.Clear();

        For Each sLocKey As String In Adventure.htblLocations.Keys
            AddNode(sLocKey);
        Next;

        For Each page As MapPage In Pages.Values
            page.SortNodes();
        Next;

        CheckForOverlaps();

        If Pages.Count = 0 Then Pages.Add(0, New MapPage(0))
#if Generator
        fGenerator.Map1.Map = Me;
#endif

    }


    public void CheckForOverlaps()
    {

        For Each page As MapPage In Pages.Values
            page.CheckForOverlaps();
        Next;

    }


    public void DeleteNode(string sKey)
    {

        private MapNode node = FindNode(sKey);
        if (node IsNot null)
        {
            Pages(node.Page).RemoveNode(node);
        }
#if Generator
        fGenerator.Map1.imgMap.Refresh();
#endif

    }


    public void RefreshNode(string sKey)
    {

        If ! Adventure.htblLocations.ContainsKey(sKey) Then Exit Sub

        private clsLocation loc = Adventure.htblLocations(sKey);
        private MapNode node = FindNode(sKey);

        If node == null Then Exit Sub

        ' Add any links out to other locations
        for (DirectionsEnum d = DirectionsEnum.North; d <= DirectionsEnum.NorthWest; d++)
        {
            if (loc.arlDirections(d).LocationKey IsNot null)
            {
                switch (d)
                {
                    case DirectionsEnum.Up:
                        {
                        node.bHasUp = true;
                    case DirectionsEnum.Down:
                        {
                        node.bHasDown = true;
                    case DirectionsEnum.In:
                        {
                        node.bHasIn = true;
                    case DirectionsEnum.Out:
                        {
                        node.bHasOut = true;
                }
            }
        Next;

    }


    public void UpdateMap(clsLocation loc)
    {

        private MapNode node = FindNode(loc.Key);

        if (node Is null)
        {
            node = AddNode(loc.Key)
#if Generator
            With fGenerator.Map1;
                .RecalculateNode(node);
                .imgMap.Refresh();
                .tabsMap.Tabs.Clear();
                For Each iPage As Integer In Pages.Keys
                    .tabsMap.Tabs.Add(iPage.ToString, Pages(iPage).Label) ' User can rename the pages;
                    'If .Page Is Nothing Then .Page = Pages(iPage)
                Next;

            }
#endif
            '            For Each d As DirectionsEnum In [Enum].GetValues(GetType(DirectionsEnum))
            '                If loc.arlDirections(d).LocationKey <> "" Then
            '                    node = AddNode(loc.Key, FindNode(loc.arlDirections(d).LocationKey), d)
            '#If Generator Then
            '                    fGenerator.Map1.RecalculateNode(node)
            '                    fGenerator.Map1.imgMap.Refresh()
            '#End If
            '                    Exit For
            '                End If
            '            Next
        Else
            node.Text = loc.ShortDescriptionSafe ' StripCarats(loc.ShortDescription.ToString);
            For Each d As DirectionsEnum In [Enum].GetValues(GetType(DirectionsEnum))
                if (loc.arlDirections(d).LocationKey <> "")
                {
                    private MapNode nodDest = FindNode(loc.arlDirections(d).LocationKey);
                    if (nodDest IsNot null)
                    {
                        if (node.Page <> nodDest.Page && d <> DirectionsEnum.In && d <> DirectionsEnum.Out)
                        {
                            'MergePages(node, nodDest)
                            ' Display arrows, since the location is on a different page
                            if (Not node.Links.ContainsKey(d))
                            {
                                private New MapLink l;
                                node.Links.Add(d, l);
                            }
                            With node.Links(d);
                                .sSource = node.Key;
                                .sDestination = node.Key ' loc.arlDirections(d).LocationKey;
                                .eSourceLinkPoint = d;
                                .eDestinationLinkPoint = OppositeDirection(d);
                                .Duplex = false;
                            }

                            ' Assuming the location on the other page actually points back at us, update it too
                            private clsLocation locDest = Adventure.htblLocations(loc.arlDirections(d).LocationKey);
                            if (locDest.arlDirections(OppositeDirection(d)).LocationKey = node.Key)
                            {
                                if (Not nodDest.Links.ContainsKey(OppositeDirection(d)) || nodDest.Links(OppositeDirection(d)).sDestination <> nodDest.Key)
                                {
                                    ' Update the destination node to make sure that it points back here
                                    UpdateMap(locDest);
                                }
                            }

                        Else
                            if (node.Links.ContainsKey(d))
                            {
                                With node.Links(d);
                                    if (.sDestination = loc.arlDirections(d).LocationKey)
                                    {
                                        ' Nothing to do
                                    Else
                                        ' If the old destination still points here, create a non-duplex link
                                        if (.Duplex && Adventure.htblLocations(.sDestination).arlDirections(.eDestinationLinkPoint).LocationKey = node.Key)
                                        {
                                            private MapNode nodeOldDest = FindNode(.sDestination);
                                            private New MapLink l;
                                            l.sSource = nodeOldDest.Key;
                                            l.sDestination = node.Key;
                                            l.eSourceLinkPoint = .eDestinationLinkPoint;
                                            l.eDestinationLinkPoint = .eSourceLinkPoint;
                                            l.Duplex = false;
                                            nodeOldDest.Links.Add(l.eSourceLinkPoint, l);
                                        }
                                        ' If the destination points at itself, then delete that link and make us a Duplex (we may have just moved back to it's page)
                                        if (nodDest.Links.ContainsKey(OppositeDirection(d)) && nodDest.Links(OppositeDirection(d)).sDestination = nodDest.Key)
                                        {
                                            nodDest.Links.Remove(OppositeDirection(d));
                                            node.Links(d).Duplex = true;
                                        }
                                        ' Then create our new link
                                        .sDestination = loc.arlDirections(d).LocationKey;
                                        If Adventure.htblLocations(.sDestination).arlDirections(.eDestinationLinkPoint).LocationKey = node.Key Then .Duplex = true
                                    }
                                    If .eSourceLinkPoint = DirectionsEnum.In Then node.bHasIn = true
                                    If .eSourceLinkPoint = DirectionsEnum.Out Then node.bHasOut = true
                                    If .eDestinationLinkPoint = DirectionsEnum.In Then nodDest.bHasIn = true
                                    If .eDestinationLinkPoint = DirectionsEnum.Out Then nodDest.bHasOut = true
                                }
                            Else
                                ' See if the destination node already links to us, non-duplex
                                private bool bFound = false;
                                For Each lDest As MapLink In nodDest.Links.Values
                                    if (lDest.sDestination = node.Key && lDest.eDestinationLinkPoint = d)
                                    {
                                        bFound = True
                                        lDest.Duplex = true;
                                    }
                                Next;
                                if (Not bFound)
                                {
                                    private New MapLink l;
                                    l.sSource = node.Key;
                                    l.sDestination = loc.arlDirections(d).LocationKey;
                                    l.eSourceLinkPoint = d;
                                    l.eDestinationLinkPoint = OppositeDirection(d);
                                    If Adventure.htblLocations(l.sDestination).arlDirections(l.eDestinationLinkPoint).LocationKey = node.Key Then l.Duplex = true
                                    node.Links.Add(d, l);
                                    If l.eSourceLinkPoint = DirectionsEnum.In Then node.bHasIn = true
                                    If l.eSourceLinkPoint = DirectionsEnum.Out Then node.bHasOut = true
                                    If l.eDestinationLinkPoint = DirectionsEnum.In Then nodDest.bHasIn = true
                                    If l.eDestinationLinkPoint = DirectionsEnum.Out Then nodDest.bHasOut = true
                                }
                            }
                        }
                    }
                Else
                    ' See if we've removed a link
                    if (node.Links.ContainsKey(d))
                    {
                        ' Ok, need to remove the link, or make it a non-duplex link from other end
                        With node.Links(d);
                            if (.Duplex)
                            {
                                ' Ok, create new non-duplex link
                                private MapNode nodeOldDest = FindNode(.sDestination);
                                private New MapLink l;
                                l.sSource = nodeOldDest.Key;
                                l.sDestination = node.Key;
                                l.eSourceLinkPoint = .eDestinationLinkPoint;
                                l.eDestinationLinkPoint = .eSourceLinkPoint;
                                l.Duplex = false;
                                nodeOldDest.Links.Add(l.eSourceLinkPoint, l);
                            }
                        }
                        node.Links.Remove(d);
                    }
                    ' Need to see if any node has a link that terminates here
                    For Each n As MapNode In Pages(node.Page).Nodes
                        For Each l As MapLink In n.Links.Values
                            if (l.sDestination = node.Key && l.eDestinationLinkPoint = d)
                            {
                                l.Duplex = false;
                            }
                        Next;
                    Next;
                }
            Next;
        }

#if Generator
        With fGenerator.Map1;
            .RecalculateNode(node);
            .imgMap.Refresh();
        }
#endif

    }


    private Point3D GetNewLocation(MapNode node, MapNode nodeFrom, DirectionsEnum dirFrom)
    {

        private New Point3D ptLocation;
        ptLocation.X = nodeFrom.Location.X;
        ptLocation.Y = nodeFrom.Location.Y;
        ptLocation.Z = nodeFrom.Location.Z;

        switch (dirFrom)
        {
            case DirectionsEnum.North:
                {
                ptLocation.Y = nodeFrom.Location.Y + nodeFrom.Height + 2;
            case DirectionsEnum.NorthEast:
                {
                ptLocation.X = nodeFrom.Location.X - node.Width - 3;
                ptLocation.Y = nodeFrom.Location.Y + nodeFrom.Height + 2;
            case DirectionsEnum.East:
                {
                ptLocation.X = nodeFrom.Location.X - node.Width - 3;
            case DirectionsEnum.SouthEast:
                {
                ptLocation.X = nodeFrom.Location.X - node.Width - 3;
                ptLocation.Y = nodeFrom.Location.Y - node.Height - 2;
            case DirectionsEnum.South:
                {
                ptLocation.Y = nodeFrom.Location.Y - node.Height - 2;
            case DirectionsEnum.SouthWest:
                {
                ptLocation.X = nodeFrom.Location.X + nodeFrom.Width + 3;
                ptLocation.Y = nodeFrom.Location.Y - node.Height - 2;
            case DirectionsEnum.West:
                {
                ptLocation.X = nodeFrom.Location.X + nodeFrom.Width + 3;
            case DirectionsEnum.NorthWest:
                {
                ptLocation.X = nodeFrom.Location.X + nodeFrom.Width + 3;
                ptLocation.Y = nodeFrom.Location.Y + nodeFrom.Height + 2;
            case DirectionsEnum.Up:
                {
                ptLocation.Z += 6;
            case DirectionsEnum.Down:
                {
                ptLocation.Z -= 6;
                'Case DirectionsEnum.In
                '    node.Page = GetNewPage()
                'Case DirectionsEnum.Out
                '    node.Page = GetNewPage()
        }

        return ptLocation;

    }


    private MapNode AddNode(string sLocKey, MapNode nodeFrom = null, DirectionsEnum dirFrom = null)
    {

        private clsLocation loc = Adventure.htblLocations(sLocKey);
        private MapNode node = FindNode(loc.Key);

        if (node Is null)
        {
            node = New MapNode

            node.Key = sLocKey;
            node.Text = loc.ShortDescriptionSafe ' StripCarats(ReplaceALRs(loc.ShortDescription.ToString));
            if (nodeFrom Is null)
            {
                node.Page = GetNewPage();
                node.Location.X = 0;
                node.Location.Y = 0;
                node.Location.Z = 0;
            Else
                node.Page = nodeFrom.Page;
                node.Location = GetNewLocation(node, nodeFrom, dirFrom);
                If dirFrom = DirectionsEnum.In || dirFrom = DirectionsEnum.Out Then node.Page = GetNewPage()
            }

            If ! Pages.ContainsKey(node.Page) Then Pages.Add(node.Page, New MapPage(node.Page))
            Pages(node.Page).AddNode(node);

            ' Add any links out to other locations
            for (DirectionsEnum d = DirectionsEnum.North; d <= DirectionsEnum.NorthWest; d++)
            {
                if (loc.arlDirections(d).LocationKey IsNot null && loc.arlDirections(d).LocationKey <> "")
                {
                    switch (d)
                    {
                        case DirectionsEnum.Up:
                            {
                            node.bHasUp = true;
                        case DirectionsEnum.Down:
                            {
                            node.bHasDown = true;
                        case DirectionsEnum.In:
                            {
                            node.bHasIn = true;
                        case DirectionsEnum.Out:
                            {
                            node.bHasOut = true;
                    }
                    AddNode(loc.arlDirections(d).LocationKey, node, OppositeDirection(d));
                    ' First, check to see if the other location already has our link
                    private MapNode nodeDest = null;
                    if (d = DirectionsEnum.In || d = DirectionsEnum.Out)
                    {
                        nodeDest = FindNode(loc.arlDirections(d).LocationKey)
                    Else
                        nodeDest = Pages(node.Page).GetNode(loc.arlDirections(d).LocationKey)
                    }

                    private MapLink link = null;
                    if (nodeDest IsNot null)
                    {
                        if (nodeDest.Links.ContainsKey(OppositeDirection(d)))
                        {
                            link = nodeDest.Links(OppositeDirection(d))
                        Else
                            For Each linkDest As MapLink In nodeDest.Links.Values
                                if (linkDest.sDestination = sLocKey)
                                {
                                    ' Make assumption that this is our link - in some layouts this may not be true
                                    link = linkDest
                                    Exit For;
                                }
                            Next;
                        }
                    }

                    if (link Is null)
                    {
                        AddLink(node, loc.arlDirections(d).LocationKey, d, OppositeDirection(d));
                    Else
                        'link.OrigPoints(link.OrigPoints.Length - 1) = ptSource
                        link.sDestination = sLocKey;
                        link.eDestinationLinkPoint = d;
                        if (DottedLink(loc.arlDirections(d)))
                        {
                            link.Style = Drawing2D.DashStyle.Dot;
                        }
                        link.Duplex = true;
                        If Pages(node.Page).GetNode(sLocKey).Anchors.ContainsKey(d) Then Pages(node.Page).GetNode(sLocKey).Anchors(d).HasLink = true
                    }
                }
            Next;

        Else
            ' Check to see if we need to merge pages
            if (nodeFrom IsNot null && node.Page <> nodeFrom.Page)
            {
                if (dirFrom <> DirectionsEnum.In && dirFrom <> DirectionsEnum.Out)
                {
                    MergePages(node, nodeFrom) ', GetNewLocation(node, nodeFrom, dirFrom));
                }
            }

        }

        return node;

    }


    internal bool DottedLink(clsDirection dir)
    {

        if (dir.Restrictions.Count > 0)
        {
            return true ' dir.bEverBeenBlocked;
        Else
            return false;
        }

    }


    private void AddLink(MapNode nodeSource, string sDest, DirectionsEnum eSourcePoint, DirectionsEnum eDestPoint)
    {
        private New MapLink link;
        private clsLocation loc = Adventure.htblLocations(nodeSource.Key);

        link.sSource = nodeSource.Key;
        link.sDestination = sDest;
        link.Duplex = false;
        if (DottedLink(loc.arlDirections(eSourcePoint)))
        {
            link.Style = Drawing2D.DashStyle.Dot;
        Else
            link.Style = Drawing2D.DashStyle.Solid;
        }
        link.eSourceLinkPoint = eSourcePoint;
        ' Make assumption that end point is opposite of this one
        link.sDestination = sDest;
        link.eDestinationLinkPoint = eDestPoint;
        If ! nodeSource.Links.ContainsKey(eSourcePoint) Then nodeSource.Links.Add(eSourcePoint, link)
        If nodeSource.Anchors.ContainsKey(eSourcePoint) Then nodeSource.Anchors(eSourcePoint).HasLink = true
    }


    public void MergePages(MapNode node1, MapNode node2)
    {

        private MapPage pageFrom;
        private MapPage pageTo;
        private MapNode nodeMoving;
        private MapNode nodeStaying;

        if (Pages(node1.Page).Nodes.Count > Pages(node2.Page).Nodes.Count || (Pages(node1.Page).Nodes.Count = Pages(node2.Page).Nodes.Count && node2.Page > node1.Page))
        {
            nodeMoving = node2
            nodeStaying = node1
        Else
            nodeMoving = node1
            nodeStaying = node2
        }
        pageFrom = Pages(nodeMoving.Page)
        pageTo = Pages(nodeStaying.Page)

        ' Work out what to offset each node by
        ' First, get correct location for
        private clsLocation locMoving = Adventure.htblLocations(nodeMoving.Key);
        private clsLocation locStaying = Adventure.htblLocations(nodeStaying.Key);
        private DirectionsEnum dirMove;

        For Each d As DirectionsEnum In [Enum].GetValues(GetType(DirectionsEnum))
            if (locMoving.arlDirections(d).LocationKey = locStaying.Key)
            {
                AddLink(nodeStaying, nodeMoving.Key, OppositeDirection(d), d);
                dirMove = d
            }
        Next;


        'AddLink(nodeStaying, nodeMoving.Key, OppositeDirection(dirMove), dirMove)

        private Point3D ptNewLocation = GetNewLocation(nodeMoving, nodeStaying, dirMove);
        private int iXOffset = nodeMoving.Location.X - ptNewLocation.X;
        private int iYOffset = nodeMoving.Location.Y - ptNewLocation.Y;
        private int iZOffset = nodeMoving.Location.Z - ptNewLocation.Z;

        For Each n As MapNode In pageFrom.Nodes
            pageTo.AddNode(n);
            n.Page = pageTo.iKey;
            n.Location.X -= iXOffset;
            n.Location.Y -= iYOffset;
            n.Location.Z -= iZOffset;
        Next;
        pageFrom.Nodes.Clear();
        Pages.Remove(pageFrom.iKey);

    }


    public void MoveNodeToPage(MapNode node, int iPage)
    {

        private MapPage page = Pages(iPage);
        private MapPage pageOld = Pages(node.Page);
        pageOld.Nodes.Remove(node);
        page.AddNode(node);
        node.Page = iPage;
        If pageOld.Nodes.Count = 0 Then Pages.Remove(pageOld.iKey)

        ' Sort out any links

    }



    internal MapNode FindNode(string sLocKey)
    {
        For Each page As MapPage In Pages.Values
            For Each node As MapNode In page.Nodes
                If node.Key = sLocKey Then Return node
            Next;
        Next;
        return null;
    }


    public int GetNewPage(bool bAllowEmptyPages = false)
    {
        private int iPage = 0;
        while (Pages.ContainsKey(iPage) && (bAllowEmptyPages || Pages(iPage).Nodes.Count > 0))
        {
            iPage += 1;
        }
        return iPage;
    }


    public void New()
    {
        Pages.Add(0, New MapPage(0));
    }

}
}