using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace ADRIFT
{

public class Map
{

    private static int iScale = 10;
    private static int iOffsetX = 200;
    private static int iOffsetY = 40 '25;
    private static int iBoundX = 0;
    private static int iBoundY = 0;
    private MapPage mPage;
    private MapPage Page { get; set; }
        {
            get
            {
            return mPage;
        }
set(ByVal MapPage)
            mPage = value
        }
    }
    private MapNode mHotTrackedNode = null;
    private MapLink mHotTrackedLink = null;
    private MapNode mActiveNode = null;
    private Anchor mHotTrackedAnchor = null;
    private MapLink mNewLink = null;
    private MapLink mSelectedLink = null;
    'Private bAllowMoveSelected As Boolean = False
    'Private bRenaming As Boolean = False
    'Private sPreviousName As String = ""
    private bool bDragged = false;
    private New MapPlanes Planes;
    private Size ' Size of the image - for some reason www gets it wrong sizeImage;

    private Color MAPBACKGROUND = Color.FromArgb(230, 255, 255);
    private Color NODEBACKGROUND = Color.FromArgb(150, 200, 255);
    private Color NODESELECTED = Color.FromArgb(255, 255, 0);
    private Color NODEBORDER = Color.FromArgb(100, 150, 200);
    private Color NODETEXT = Color.FromArgb(0, 0, 0);
    private Color LINKCOLOUR = Color.FromArgb(70, 0, 0);
    private Color LINKSELECTED = Color.FromArgb(200, 150, 0);


    private bool bLockMapCentre;
    internal bool LockMapCentre { get; set; }
        {
            get
            {
            return bLockMapCentre;
        }
set(ByVal Boolean)
            if (value <> bLockMapCentre)
            {
                bLockMapCentre = value
                If bLockMapCentre Then LockPlayerCentre = false
#if Runner
#if Mono Or www

#else
                (Infragistics.Win.UltraWinToolbars.StateButtonTool)(fRunner.UTMMain.Tools("CentreMapLock")).Checked = bLockMapCentre;
#endif

                SaveSetting("ADRIFT", "Runner", "CentreMapLock", CInt(value).ToString);
#endif
            }
        }
    }


    private bool bLockPlayerCentre;
    internal bool LockPlayerCentre { get; set; }
        {
            get
            {
            return bLockPlayerCentre;
        }
set(ByVal Boolean)
            if (value <> bLockPlayerCentre)
            {
                bLockPlayerCentre = value
                If bLockPlayerCentre Then LockMapCentre = false
#if Runner
#if Mono Or www
#else
                (Infragistics.Win.UltraWinToolbars.StateButtonTool)(fRunner.UTMMain.Tools("MapPlayerLock")).Checked = bLockPlayerCentre;
#endif
                SaveSetting("ADRIFT", "Runner", "MapPlayerLock", CInt(value).ToString);
#endif
            }
        }
    }


private class MapPlanes
    {
        Inherits Generic.Dictionary(Of Integer, MapPlane);

        private void CheckExists(int Z)
        {
            if (Not ContainsKey(Z))
            {
                private New MapPlane(Z) planeNew;
                Add(Z, planeNew);
            }
        }


        public Point GetPoint2D(Point3D point)
        {
            CheckExists(point.Z);
            return Item(point.Z).GetPointOnPlane(point.X, point.Y);
        }
        public Point GetPoint2D(double X, double Y, int Z)
        {
            CheckExists(Z);
            return Item(Z).GetPointOnPlane(X, Y);
        }


        public Matrix GetMatrix(int Z)
        {
            CheckExists(Z);
            return Item(Z).matrix;
        }

    }


private class MapPlane
    {

        public int Z;
        Private pt0, pt1, pt2, pt3, pt4 As Point
        private const int SIZE = 1000;
        internal Matrix matrix;

        public void New(int Z)
        {

            ' We only need to calculate 3 of the points, because we are a parallellogram, so the 4th is trivial
            ' From these, we can interpolate any point on the plane

            pt1 = TranslateToScreen(New Point3D(0 - (0 * Z), 0, Z))
            pt2 = TranslateToScreen(New Point3D(SIZE - (0 * Z), 0, Z))
            pt3 = TranslateToScreen(New Point3D(SIZE - (0 * Z), SIZE - (0 * Z), Z))

            ' Create a matrix for any graphics transformations on our plane
            matrix = New Matrix()
            private double dfRotationAngle = Math.Atan((pt2.Y - pt1.Y) / (pt2.X - pt1.X)) ' in Radians;

            matrix.Translate(CSng(pt1.X), CSng(pt1.Y));

            private int s1 = pt3.Y - pt2.Y;
            private int s2 = pt2.X - pt3.X;
            private double dfRotationPlusSquewAngle = Math.Atan(s2 / s1);
            private double dfSquewAngle = dfRotationPlusSquewAngle - dfRotationAngle;
            private double dfYY = Math.Sqrt(s1 ^ 2 + s2 ^ 2) ' Length of line of what was height;
            private double dfXX = Math.Sqrt((pt2.X - pt1.X) ^ 2 + (pt2.Y - pt1.Y) ^ 2) ' Length of line of what was width;
            private double dfSquewLength = Math.Sin(dfSquewAngle) * dfYY;
            private double dfPreSquewHeight = Math.Cos(dfSquewAngle) * dfYY;
            private float dfXShear = CSng(-dfSquewLength / dfPreSquewHeight);

            matrix.Rotate(CSng(dfRotationAngle * (180 / Math.PI)));

            If ! Double.IsInfinity(dfXShear) && ! Double.IsNaN(dfXShear) Then matrix.Shear(dfXShear, 0)

            private float dfXScale = CSng(dfXX / SIZE / iScale);
            private float dfYScale = CSng(dfPreSquewHeight / SIZE / iScale);
            If dfXScale <> 0 || dfYScale <> 0 Then matrix.Scale(dfXScale, dfYScale)
            'Debug.WriteLine(String.Format("Translation: {0}, {1}, Rotation: {2}, Scale: {3}, {4}", pt1.X, pt2.X, dfRotationAngle, dfXScale, dfYScale))

        }


        public Point GetPointOnPlane(int X, int Y)
        {

            private int iX1 = (pt2.X - pt1.X) * X '- (Z * 50000);
            private int iY1 = (pt2.Y - pt1.Y) * X ' - (Z * 50000);
            private int iX2 = (pt3.X - pt2.X) * Y '+ (Z * 50000) ' + (Z * 50000);
            private int iY2 = (pt3.Y - pt2.Y) * Y '+ (Z * 50000) ' + (Z * 50000);

            return new Point(pt1.X + CInt((iX1 + iX2) / SIZE), pt1.Y + CInt((iY1 + iY2) / SIZE));

        }
        public Point GetPointOnPlane(double X, double Y)
        {

            private double dfX1 = (pt2.X - pt1.X) * X '- (Z * 50000);
            private double dfY1 = (pt2.Y - pt1.Y) * X '- (Z * 50000);
            private double dfX2 = (pt3.X - pt2.X) * Y '+ (Z * 50000) '+ (Z * 50000);
            private double dfY2 = (pt3.Y - pt2.Y) * Y '+ (Z * 50000) '+ (Z * 50000);

            return new Point(pt1.X + CInt((dfX1 + dfX2) / SIZE), pt1.Y + CInt((dfY1 + dfY2) / SIZE));

        }

    }


private class clsSelectedNodes
    {
        Inherits Generic.List(Of MapNode);

        ' Returns True if new item added
        Shadows Function Add(ByVal item As MapNode, Optional ByVal bRefresh As Boolean = true) As Boolean;
            if (Not MyBase.Contains(item))
            {
                MyBase.Add(item);
                RefreshMap(bRefresh);
                return true;
            Else
                return false;
            }
        }

        Shadows Sub Remove(ByVal item As MapNode, Optional ByVal bRefresh As Boolean = true);
            if (MyBase.Contains(item))
            {
                MyBase.Remove(item);
                RefreshMap(bRefresh);
            }
        }

        Shadows Sub RemoveAt(ByVal index As Integer, Optional ByVal bRefresh As Boolean = true);
            if (index < MyBase.Count)
            {
                MyBase.RemoveAt(index);
                RefreshMap(bRefresh);
            }
        }

        private void RefreshMap(bool bRefresh)
        {
#if Generator
            If bRefresh Then fGenerator.Map1.imgMap.Refresh()
#endif
        }

    }


    private New clsSelectedNodes SelectedNodes;

    private bool bShowAxes = true;
    public bool ShowAxes { get; set; }
        {
            get
            {
#if Runner
            return false;
#else
            return bShowAxes;
#endif
        }
set(ByVal Boolean)
            if (value <> bShowAxes)
            {
                bShowAxes = value
                imgMap.Refresh();
                SaveSetting("ADRIFT", "Generator", "ShowAxes", CInt(bShowAxes).ToString);
            }
        }
    }

    private bool bShowGrid = true;
    public bool ShowGrid { get; set; }
        {
            get
            {
#if Runner
            return false;
#else
            return bShowGrid;
#endif
        }
set(ByVal Boolean)
            if (value <> bShowGrid)
            {
                bShowGrid = value
                imgMap.Refresh();
                SaveSetting("ADRIFT", "Generator", "ShowGrid", CInt(bShowGrid).ToString);
            }
        }
    }

    private MapNode HotTrackedNode { get; set; }
        {
            get
            {
            return mHotTrackedNode;
        }
set(ByVal MapNode)
            if (value IsNot mHotTrackedNode)
            {
                mHotTrackedNode = value
                imgMap.Refresh();
            }
        }
    }


    private MapLink HotTrackedLink { get; set; }
        {
            get
            {
            return mHotTrackedLink;
        }
set(ByVal MapLink)
            if (value IsNot mHotTrackedLink)
            {
                mHotTrackedLink = value
                imgMap.Refresh();
            }
        }
    }


    private MapLink NewLink { get; set; }
        {
            get
            {
            return mNewLink;
        }
set(ByVal MapLink)
            if (value IsNot mNewLink)
            {
                if (mNewLink IsNot null)
                {
                    ' Hide the anchors
                    private MapNode nodeSource = Page.GetNode(mNewLink.sSource);
                    If nodeSource != ActiveNode Then nodeSource.Anchors(mNewLink.eSourceLinkPoint).Visible = false
                    private MapNode nodeDest = Page.GetNode(mNewLink.sDestination);
                    If nodeDest != null && nodeDest != ActiveNode Then nodeDest.Anchors(mNewLink.eDestinationLinkPoint).Visible = false
                }
                mNewLink = value
                If SelectedLink == value Then SelectedLink = null
                imgMap.Refresh();
            }
        }
    }


    private MapLink SelectedLink { get; set; }
        {
            get
            {
            return mSelectedLink;
        }
set(ByVal MapLink)
            if (value IsNot mSelectedLink)
            {
                if (mSelectedLink IsNot null)
                {
                    ' Hide the anchors
                    private MapNode nodeSource = Page.GetNode(mSelectedLink.sSource);
                    If nodeSource != null && nodeSource != ActiveNode Then nodeSource.Anchors(mSelectedLink.eSourceLinkPoint).Visible = false
                    private MapNode nodeDest = Page.GetNode(mSelectedLink.sDestination);
                    If nodeDest != null && nodeDest != ActiveNode && nodeDest.Anchors.ContainsKey(mSelectedLink.eDestinationLinkPoint) Then nodeDest.Anchors(mSelectedLink.eDestinationLinkPoint).Visible = false
                }
                mSelectedLink = value
                If value != null Then ActiveNode = null
                imgMap.Refresh();
            }
        }
    }


    private Anchor HotTrackedAnchor { get; set; }
        {
            get
            {
            return mHotTrackedAnchor;
        }
set(ByVal Anchor)
            if (value IsNot mHotTrackedAnchor)
            {
                mHotTrackedAnchor = value
                imgMap.Refresh();
            }
        }
    }


    private MapNode ActiveNode { get; set; }
        {
            get
            {
            return mActiveNode;
        }
set(ByVal MapNode)
            if (value IsNot mActiveNode)
            {
                mActiveNode = value
                If value != null Then RecalculateNode(value)
                For Each node As MapNode In Page.Nodes
                    if (node IsNot ActiveNode && node.Anchors.Count > 0)
                    {
                        'node.Anchors.Clear()
                        For Each a As Anchor In node.Anchors.Values
                            a.Visible = false;
                        Next;
                        'RecalculateNode(node)
                    }
                Next;
                If value != null Then SelectedLink = null
                'bAllowMoveSelected = False
                'bRenaming = False
                txtRename.Visible = false;
            }
            If value != null && ! SelectedNodes.Add(value) Then imgMap.Refresh()
        }
    }



    private clsMap oMap;
    public clsMap Map { get; set; }
        {
            get
            {
            return oMap;
        }
set(ByVal clsMap)
            oMap = value
            LoadMap();
        }
    }


    private bool bLoading = false;
    private void LoadMap()
    {

        try
        {
            bLoading = True
#if Mono
        tabsMap.TabPages.Clear();
#elif Not www
            try
            {
                tabsMap.Tabs.Clear();
            Catch
            }
#endif

            SelectedLink = Nothing
            ActiveNode = Nothing
            NewLink = Nothing
            Page = Nothing

            If Map == null Then Exit Sub
            For Each iPage As Integer In Map.Pages.Keys
#if Mono
            tabsMap.TabPages.Add(iPage.ToString, Map.Pages(iPage).Label) ' User can rename the pages;
            tabsMap.TabPages.RemoveByKey(iPage.ToString) ' .Visible = false;
#elif Not www
                tabsMap.Tabs.Add(iPage.ToString, Map.Pages(iPage).Label) ' User can rename the pages;
#if Runner
            tabsMap.Tabs(iPage.ToString).Visible = false;
#endif
#endif
                If Page == null Then Page = Map.Pages(iPage)
            Next;
            if (Page IsNot null)
            {
                CentreMap();
                'RecalculateNodes()
            }
            bLoading = False
#if Mono OrElse www
        If Map.SelectedPage <> "" && tabsMap.TabPages.ContainsKey(Map.SelectedPage) Then tabsMap.SelectedTab = tabsMap.TabPages(Map.SelectedPage)
        if (tabsMap.SelectedTab IsNot null)
        {
            'tabsMap.SelectedTab.Controls.Add(Me.imgMap)
            imgMap.Parent = tabsMap.SelectedTab;
        ElseIf tabsMap.TabPages.Count > 0 Then
            imgMap.Parent = tabsMap.TabPages(0);
        Else
            imgMap.Parent = tabsMap.Parent;
        }
#else
            If Map.SelectedPage <> "" && tabsMap.Tabs.Exists(Map.SelectedPage) Then tabsMap.SelectedTab = tabsMap.Tabs(Map.SelectedPage)
#endif
        }
        catch (Exception ex)
        {
            ErrMsg("Error loading map", ex);
        }

    }


    private void imgMap_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
    {

        imgMap.Focus();

#if Generator
        If ! bDragged Then ' e.Button = Windows.Forms.MouseButtons.Left &&
            if (HotTrackedAnchor IsNot null)
            {
                if (TypeOf HotTrackedAnchor.Parent Is MapNode)
                {
                    private MapNode parNode = CType(HotTrackedAnchor.Parent, MapNode);
                    if (NewLink IsNot null)
                    {
                        ' We are selecting an end-point
                        if (Not (NewLink.sSource Is null || parNode.Key = NewLink.sSource && HotTrackedAnchor.Direction = NewLink.eSourceLinkPoint) Then ' HotTrackedAnchor.Parent.Key IsNot NewLink.sDestination && HotTrackedAnchor.Direction <> NewLink.eSourceLinkPoint)
                        {
                            ' Different point
                            NewLink.sDestination = parNode.Key;
                            NewLink.eDestinationLinkPoint = HotTrackedAnchor.Direction;
                            ReDim NewLink.OrigMidPoints(-1);
                            parNode.Anchors(HotTrackedAnchor.Direction).HasLink = true;
                            Page.GetNode(NewLink.sSource).Anchors(NewLink.eSourceLinkPoint).HasLink = true;
                            HotTrackedAnchor.Visible = false;
                            ' Add the link in the locations
                            ' If the destination link point has an outgoing route, then don't make it duplex
                            Adventure.htblLocations(NewLink.sSource).arlDirections(NewLink.eSourceLinkPoint).LocationKey = NewLink.sDestination;
                            if (Adventure.htblLocations(NewLink.sDestination).arlDirections(NewLink.eDestinationLinkPoint).LocationKey = "")
                            {
                                Adventure.htblLocations(NewLink.sDestination).arlDirections(NewLink.eDestinationLinkPoint).LocationKey = NewLink.sSource;
                                NewLink.Duplex = true;
                            Else
                                NewLink.Duplex = false;
                            }
                            Adventure.Changed = true;

                            NewLink = Nothing
                        Else
                            ' Same point - just remove the link
                            RemoveLink(NewLink);
                        }
                        ActiveNode = Nothing
                    Else
                        ' Select any link attached to this anchor
                        private MapLink link = null;
                        if (parNode.Links.ContainsKey(HotTrackedAnchor.Direction))
                        {
                            link = parNode.Links(HotTrackedAnchor.Direction)
                        Else
                            ' Look for a link terminating here
                            For Each node As MapNode In Page.Nodes
                                For Each linkOther As MapLink In node.Links.Values
                                    if (linkOther.sDestination = parNode.Key && linkOther.eDestinationLinkPoint = HotTrackedAnchor.Direction)
                                    {
                                        link = linkOther
                                        Exit For;
                                    }
                                Next;
                            Next;
                        }
                        SelectedLink = link
                        If e.Button = Windows.Forms.MouseButtons.Right && SelectedLink != null Then cmsLink.Show(imgMap, imgMap.PointToClient(MousePosition))
                        imgMap.Refresh();
                    }
                Else
                    ' Link anchor

                }
            }

            if (e.Button = Windows.Forms.MouseButtons.Left)
            {
                if (HotTrackedNode Is null)
                {
                    SelectedNodes.Clear();
                    ActiveNode = Nothing
                }
                If HotTrackedAnchor == null Then SelectedLink = null
                imgMap.Refresh();
            }

        }

#endif

    }


    private void imgMap_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
    {
#if Generator
        fGenerator.sDestinationList = "";
#endif
        if (HotTrackedAnchor IsNot null)
        {
            if (TypeOf HotTrackedAnchor.Parent Is MapNode)
            {
                private MapNode nodParent = CType(HotTrackedAnchor.Parent, MapNode);

                if (nodParent.Links.ContainsKey(HotTrackedAnchor.Direction))
                {
                    RemoveLink(nodParent.Links(HotTrackedAnchor.Direction));
                Else
                    ' Check to see if a link exists from another location - if so, edit that link
                    For Each node As MapNode In Map.Pages(nodParent.Page).Nodes
                        For Each link As MapLink In node.Links.Values
                            if (link.sDestination = nodParent.Key && link.eDestinationLinkPoint = HotTrackedAnchor.Direction)
                            {
                                nodParent = node
                                HotTrackedAnchor = node.Anchors(link.eSourceLinkPoint)
                                RemoveLink(node.Links(link.eSourceLinkPoint));
                                Exit For;
                            }
                        Next;
                    Next;
                }

                NewLink = New MapLink
                NewLink.sSource = nodParent.Key;
                NewLink.eSourceLinkPoint = HotTrackedAnchor.Direction;
                NewLink.Duplex = true;
                if (nodParent.Links.ContainsKey(NewLink.eSourceLinkPoint))
                {
                    NewLink = nodParent.Links(NewLink.eSourceLinkPoint)
                    NewLink.Anchors.Clear();
                    ReDim NewLink.OrigMidPoints(-1);
                Else
                    nodParent.Links.Add(NewLink.eSourceLinkPoint, NewLink);
                }
                for (DirectionsEnum d = DirectionsEnum.NorthWest; d <= DirectionsEnum.North; d += CType(-1, DirectionsEnum))
                {
                    If d <> HotTrackedAnchor.Direction && nodParent.Anchors.ContainsKey(d) Then nodParent.Anchors(d).Visible = false
                Next;
                RecalculateLinks(nodParent);
            }
        ElseIf HotTrackedNode != null Then
            EditLocation();
        Else
            AddNode();
        }
    }


    private void AddNode(bool bCentre = false)
    {

        private New clsLocation loc;
        loc.Key = loc.GetNewKey;
        loc.ShortDescription = New Description("New Location");
        Adventure.htblLocations.Add(loc, loc.Key);

        private New MapNode node;
        private int Z = 0;
        If ActiveNode != null Then Z = ActiveNode.Location.Z
        private Point3D pt;
        if (Not bCentre)
        {
            pt = MouseTo3DCoords(Z)
        Else
            private New Point(imgMap.Location.X + CInt(sizeImage.Width / 2), imgMap.Location.Y + CInt(sizeImage.Height / 2)) ptCentre;
            private int iX = ptCentre.X + iBoundX;
            private int iY = ptCentre.Y + iBoundY;
            private Point pt2 = ConvertScreento3D(new Point(iX, iY), Z * iScale);

            pt = New Point3D(CInt(pt2.X / iScale), CInt(pt2.Y / iScale), Z)
        }

        node.Location.X = CInt(pt.X - node.Width / 2);
        node.Location.Y = CInt(pt.Y - node.Height / 2);
        node.Location.Z = Z;
        'End If
        node.Key = loc.Key;
        node.Text = loc.ShortDescriptionSafe ' StripCarats(ReplaceALRs(loc.ShortDescription.ToString));
        node.Page = Page.iKey;

        Page.AddNode(node);
        ActiveNode = node

        RecalculateNode(node);
        imgMap.Refresh();

        UpdateLocDescription(loc.Key, loc.ShortDescription.ToString);

        RenameNode();
#if Generator
        Adventure.Changed = true;
#endif

    }


    private void UpdateLocDescription(string sKey, string sShortDesc)
    {
#if Generator
        UpdateListItem(sKey, sShortDesc);
#endif
    }
    private void EditLocation()
    {
        if (SelectedNodes IsNot null && SelectedNodes.Count = 1)
        {
            private clsLocation loc;
            loc = Adventure.htblLocations(SelectedNodes(0).Key)
            If loc == null Then Exit Sub
            loc.EditItem();
        }
    }

    private void imgMap_MouseEnter(object sender, System.EventArgs e)
    {
        'imgMap.Focus()
    }


    internal void PaintMe(Size size)
    {

        LockPlayerCentre = True

        if (size <> sizeImage)
        {
            'If imgMap.Image.Size <> size OrElse imgMap.Size <> size Then
            imgMap.Image = New Bitmap(size.Width, size.Height);
            imgMap.Size = size;
            sizeImage = size
            CentreMap();
        }
        'If Me.Size <> size Then Me.Size = size
        'If imgMap.Size <> size Then imgMap.Size = size

        private Graphics g = Graphics.FromImage(imgMap.Image);
        g.Clear(MAPBACKGROUND);
        RecalculateNodes();

        'CentreMap()
        'iBoundY = -(size.Height / 2)
        'iBoundX = -(size.Width / 2)
        'RecalculateNodes()
        imgMap_Paint(null, New PaintEventArgs(g, New Rectangle(0, 0, size.Width, size.Height)));
    }
    private void imgMap_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
    {

        If Page == null Then Exit Sub

        PaintGraphics(e.Graphics);
    }
    private void PaintGraphics(Graphics gfx)
    {

#if Not www
        if (MouseButtons = Windows.Forms.MouseButtons.Left && (Control.ModifierKeys And Keys.Shift) > 0)
        {
            private New Pen(Color.Black, 1) penLasso;
            penLasso.DashStyle = DashStyle.DashDot;
            private int x1 = Math.Min(ptStartLasso.X, ptEndLasso.X);
            private int y1 = Math.Min(ptStartLasso.Y, ptEndLasso.Y);
            private int x2 = Math.Max(ptStartLasso.X, ptEndLasso.X);
            private int y2 = Math.Max(ptStartLasso.Y, ptEndLasso.Y);
            gfx.DrawRectangle(penLasso, x1, y1, Math.Abs(x1 - x2), Math.Abs(y1 - y2));
        }
#endif

        With Page;
            gfx.SmoothingMode = SmoothingMode.HighQuality ' SmoothingMode.HighQuality;
            If ShowGrid Then DrawGrid(gfx)
            If ShowAxes Then DrawAxes(gfx)
            For Each node As MapNode In .Nodes
                DrawNode(gfx, node);
            Next;
        }

    }


    private void RecalculateNodes(int iPage = -1)
    {
        if (Map IsNot null && Map.Pages.Count > 0 && tabsMap.SelectedTab IsNot null)
        {
            if (iPage = -1)
            {
#if Mono Then 'Or www
                iPage = SafeInt(tabsMap.SelectedTab.Name)
#elif Not www
                iPage = SafeInt(tabsMap.SelectedTab.Key)
#else
                private MapNode node = Map.FindNode(Adventure.Player.Location.LocationKey);
                If node == null Then Exit Sub

                iPage = node.Page
                if (Page Is null || Page.iKey <> iPage)
                {
                    Page = Map.Pages(iPage)
                    CentreMap();
                }
#endif
            }
            if (Map.Pages.ContainsKey(iPage))
            {
                With Map.Pages(iPage);
                    Planes.Clear();
                    For Each node As MapNode In .Nodes
                        RecalculateNode(node, false);
                    Next;
                    For Each node As MapNode In .Nodes
                        RecalculateLinks(node);
                    Next;
                    ' Need to call RecalculateLinks on any nodes on a different page that link to us
                    For Each node As MapNode In .Nodes
                        if (node.bHasIn || node.bHasOut)
                        {
                            For Each p As MapPage In Map.Pages.Values
                                if (p.iKey <> iPage)
                                {
                                    For Each nodeOther As MapNode In p.Nodes
                                        For Each l As MapLink In nodeOther.Links.Values
                                            if (l.sDestination = node.Key)
                                            {
                                                RecalculateLinks(nodeOther);
                                            }
                                        Next;
                                    Next;
                                }
                            Next;
                        }
                    Next;
                }
            }
        }
    }


    private static Point TranslateToScreen(Point3D pt3D)
    {

        private int X = pt3D.X * iScale;
        private int Y = pt3D.Y * iScale;
        private int Z = pt3D.Z * iScale;

        private Point pt2D = Convert3DtoScreen(X, Y, Z);
        pt2D.X -= iBoundX;
        pt2D.Y -= iBoundY;

        return pt2D;

    }


    private Point GetLinkPoint(MapNode node, DirectionsEnum d, MapNode nodDest = null)
    {

        private Point pt;

        switch (d)
        {
            case DirectionsEnum.North:
                {
                'pt = New Point(node.Points(0).X + CInt((node.Points(1).X - node.Points(0).X) / 2), node.Points(0).Y)
                pt = Planes.GetPoint2D(node.Location.X + node.Width / 2, node.Location.Y, node.Location.Z)
            case DirectionsEnum.NorthEast:
                {
                pt = node.Points(1)
            case DirectionsEnum.East:
                {
                'pt = New Point(node.Points(1).X + CInt((node.Points(2).X - node.Points(1).X) / 2), node.Points(1).Y + CInt((node.Points(2).Y - node.Points(1).Y) / 2))
                pt = Planes.GetPoint2D(node.Location.X + node.Width, node.Location.Y + node.Height / 2, node.Location.Z)
            case DirectionsEnum.SouthEast:
                {
                pt = node.Points(2)
            case DirectionsEnum.South:
                {
                'pt = New Point(node.Points(3).X + CInt((node.Points(2).X - node.Points(3).X) / 2), node.Points(2).Y)
                pt = Planes.GetPoint2D(node.Location.X + node.Width / 2, node.Location.Y + node.Height, node.Location.Z)
            case DirectionsEnum.SouthWest:
                {
                pt = node.Points(3)
            case DirectionsEnum.West:
                {
                'pt = New Point(node.Points(0).X + CInt((node.Points(3).X - node.Points(0).X) / 2), node.Points(0).Y + CInt((node.Points(3).Y - node.Points(0).Y) / 2))
                pt = Planes.GetPoint2D(node.Location.X, node.Location.Y + node.Height / 2, node.Location.Z)
            case DirectionsEnum.NorthWest:
                {
                pt = node.Points(0)
            case DirectionsEnum.Up:
            case DirectionsEnum.Down:
                {
                'pt = New Point(node.Points(0).X + CInt((node.Points(2).X - node.Points(0).X) / 2), node.Points(0).Y + CInt((node.Points(2).Y - node.Points(0).Y) / 2))
                pt = Planes.GetPoint2D(node.Location.X + node.Width / 2, node.Location.Y + node.Height / 2, node.Location.Z)
            case DirectionsEnum.In:
                {
                ' This will depend where the destination is
                if (nodDest IsNot null && nodDest.Page = node.Page)
                {
                    if (nodDest.Location.X > node.Location.X + node.Width)
                    {
                        pt = Planes.GetPoint2D(node.Location.X + node.Width, node.Location.Y + node.Height / 4, node.Location.Z)
                        node.eInEdge = DirectionsEnum.East;
                    ElseIf nodDest.Location.X + nodDest.Width < node.Location.X Then
                        pt = Planes.GetPoint2D(node.Location.X, node.Location.Y + (3 * node.Height / 4), node.Location.Z)
                        node.eInEdge = DirectionsEnum.West;
                    ElseIf nodDest.Location.Y > node.Location.Y Then
                        pt = Planes.GetPoint2D(node.Location.X + (3 * node.Width / 4), node.Location.Y + node.Height, node.Location.Z)
                        node.eInEdge = DirectionsEnum.South;
                    Else
                        pt = Planes.GetPoint2D(node.Location.X + node.Width / 4, node.Location.Y, node.Location.Z)
                        node.eInEdge = DirectionsEnum.North;
                    }
                Else
                    'pt = Planes.GetPoint2D(node.Location.X + node.Width, node.Location.Y + node.Height / 4, node.Location.Z)
                }
            case DirectionsEnum.Out:
                {
                '' This will depend where the destination is
                'If nodDest IsNot Nothing AndAlso nodDest.Page = node.Page Then
                'Else
                '    pt = Planes.GetPoint2D(node.Location.X + node.Width, node.Location.Y + (3 * node.Height / 4), node.Location.Z)
                'End If
                if (nodDest IsNot null && nodDest.Page = node.Page)
                {
                    if (nodDest.Location.X > node.Location.X + node.Width)
                    {
                        pt = Planes.GetPoint2D(node.Location.X + node.Width, node.Location.Y + (3 * node.Height / 4), node.Location.Z)
                        node.eOutEdge = DirectionsEnum.East;
                    ElseIf nodDest.Location.X + nodDest.Width < node.Location.X Then
                        pt = Planes.GetPoint2D(node.Location.X, node.Location.Y + node.Height / 4, node.Location.Z)
                        node.eOutEdge = DirectionsEnum.West;
                    ElseIf nodDest.Location.Y > node.Location.Y Then
                        pt = Planes.GetPoint2D(node.Location.X + node.Width / 4, node.Location.Y + node.Height, node.Location.Z)
                        node.eOutEdge = DirectionsEnum.South;
                    Else
                        pt = Planes.GetPoint2D(node.Location.X + (3 * node.Width / 4), node.Location.Y, node.Location.Z)
                        node.eOutEdge = DirectionsEnum.North;
                    }
                Else
                    'pt = Planes.GetPoint2D(node.Location.X + node.Width, node.Location.Y + node.Height / 4, node.Location.Z)
                }
        }

        return pt;

    }


    private void RecalculateLinks(MapNode node)
    {

        If Map == null Then Exit Sub

        For Each link As MapLink In node.Links.Values
            With link;
                If Page == null Then Page = Map.Pages(node.Page)
                private MapNode nodDest = Page.GetNode(.sDestination);
                private Point ptStart = GetLinkPoint(node, .eSourceLinkPoint, nodDest);
                private Point ptEnd;
                private int iDist;


                if (nodDest IsNot null)
                {
                    ptEnd = GetLinkPoint(nodDest, .eDestinationLinkPoint, node)
                    iDist = CInt(Math.Sqrt(Math.Max(Math.Abs(ptStart.X - ptEnd.X) ^ 2 + Math.Abs(ptStart.Y - ptEnd.Y) ^ 2, 1)))
                    'Debug.WriteLine(iDist)
                    If .eDestinationLinkPoint = DirectionsEnum.In && nodDest.CompareTo(node) > 0 Then nodDest.bDrawIn = true
                    If .eDestinationLinkPoint = DirectionsEnum.Out && nodDest.CompareTo(node) > 0 Then nodDest.bDrawOut = true
                Else
                    ' Create a set distance for the assister if we don't have 2 nodes to compare
                    iDist = (node.Points(1).X - node.Points(0).X) * 3
                }

                ' If we have user defined points, don't add the assisters...
                private int iMidStart = 2;
                if (nodDest Is null)
                {
                    ReDim .Points(2) ' 2 points for start, one for end;
                Else
                    if (.OrigMidPoints.Length = 0 || link Is NewLink)
                    {
                        ReDim .Points(3) ' 2 points for each end;
                    Else
                        ReDim .Points(.OrigMidPoints.Length + 1) ' 1 point for each end, plus any mid-points;
                        iMidStart = 1
                    }
                }

                'ReDim .Points(1 + CInt(IIf(link IsNot NewLink OrElse .sDestination = "", .OrigMidPoints.Length, 0)) + CInt(IIf(.sDestination <> "", 2, 0)))
                .Points(0) = ptStart;
                If .OrigMidPoints.Length = 0 || link == NewLink Then .Points(1) = GetBezierAssister(node, .eSourceLinkPoint, iDist)

                if (link IsNot NewLink || .sDestination = "")
                {
                    for (int i = 0; i <= .OrigMidPoints.Length - 1; i++)
                    {
                        private Point ptMid = TranslateToScreen(.OrigMidPoints(i));
                        .Points(iMidStart + i) = ptMid;
                        if (link IsNot NewLink)
                        {
                            With link.Anchors(i);
                                private New MapNode(False) n;
                                n.Location = link.OrigMidPoints(i) ' change to get rid of node calc;
                                RecalculateNode(n);
                                .Points = GetAnchorPoints(n, 0, 0);
                            }
                        }
                    Next;
                }

                ' 2 points for the end
                if (nodDest IsNot null)
                {
                    'Dim nodDest As MapNode = Page.GetNode(.sDestination)
                    'Dim ptEnd As Point = GetLinkPoint(nodDest, .eDestinationLinkPoint)
                    If .Points.Length = 4 && (.OrigMidPoints.Length = 0 || link == NewLink) Then .Points(.Points.Length - 2) = GetBezierAssister(nodDest, .eDestinationLinkPoint, iDist)
                    .Points(.Points.Length - 1) = ptEnd;
                }

                if (link.eSourceLinkPoint = DirectionsEnum.In && link.sDestination <> "")
                {
                    If nodDest == null Then nodDest = Map.FindNode(link.sDestination)
                    If nodDest != null && link.Duplex Then nodDest.bDrawOut = true
                }
                if (link.eSourceLinkPoint = DirectionsEnum.Out && link.sDestination <> "")
                {
                    If nodDest == null Then nodDest = Map.FindNode(link.sDestination)
                    If nodDest != null && link.Duplex Then nodDest.bDrawIn = true
                }
                'Debug.WriteLine(String.Format("Points count: {0}, OrigPoints len: {1}, sDest: {2}", .Points.Length, .OrigMidPoints.Length, .sDestination))
            }
        Next;

        'node.bDrawIn = False
        'node.bDrawOut = False
        if (node.bHasOut)
        {
            'Dim iCircleWidth As Integer = CInt(iScale * 6 / node.Width)
            switch (node.eOutEdge)
            {
                case DirectionsEnum.North:
                    {
                    node.ptOut = New Point(CInt(3 * node.Width * iScale / 4), 0);
                case DirectionsEnum.East:
                    {
                    node.ptOut = New Point(node.Width * iScale, CInt(3 * node.Height * iScale / 4));
                case DirectionsEnum.South:
                    {
                    node.ptOut = New Point(CInt(node.Width * iScale / 4), node.Height * iScale);
                case DirectionsEnum.West:
                    {
                    node.ptOut = New Point(0, CInt(node.Height * iScale / 4));
            }
            'Dim rectInOut As New Rectangle(X2 - X - CInt(iCircleWidth / 2), CInt((Y2 - Y) / 4 - (iCircleWidth / 2)), iCircleWidth, iCircleWidth)
        }
        if (node.bHasIn)
        {
            'Dim iCircleWidth As Integer = CInt(iScale * 6 / node.Width)
            switch (node.eInEdge)
            {
                case DirectionsEnum.North:
                    {
                    node.ptIn = New Point(CInt(node.Width * iScale / 4), 0);
                case DirectionsEnum.East:
                    {
                    node.ptIn = New Point(node.Width * iScale, CInt(node.Height * iScale / 4));
                case DirectionsEnum.South:
                    {
                    node.ptIn = New Point(CInt(3 * node.Width * iScale / 4), node.Height * iScale);
                case DirectionsEnum.West:
                    {
                    node.ptIn = New Point(0, CInt(3 * node.Height * iScale / 4));
            }
            'Dim rectInOut As New Rectangle(X2 - X - CInt(iCircleWidth / 2), CInt((Y2 - Y) / 4 - (iCircleWidth / 2)), iCircleWidth, iCircleWidth)
        }

    }



    ' Recalculate the node points
    public void RecalculateNode(MapNode node, bool bRecalculateLinks = true)
    {

        If node == null || node.Key == null Then Exit Sub
#if Runner
        node.Seen = Adventure.Player.HasSeenLocation(node.Key);
#endif

        Dim points(3) As Point

        points(0) = Planes.GetPoint2D(node.Location);
        points(1) = Planes.GetPoint2D(New Point3D(node.Location.X + node.Width, node.Location.Y, node.Location.Z));
        points(2) = Planes.GetPoint2D(New Point3D(node.Location.X + node.Width, node.Location.Y + node.Height, node.Location.Z));
        points(3) = Planes.GetPoint2D(New Point3D(node.Location.X, node.Location.Y + node.Height, node.Location.Z));

        node.Points = New Point() {points(0), points(1), points(2), points(3)}

        node.ptUp = Planes.GetPoint2D(node.Location.X + node.Width / 2, node.Location.Y + node.Height / 2, node.Location.Z - 6);
        node.ptDown = Planes.GetPoint2D(node.Location.X + node.Width / 2, node.Location.Y + node.Height / 2, node.Location.Z + 6);

        'node.bDrawIn = False
        'node.bDrawOut = False


        If node.Anchors.Count > 0 Then ' because otherwise we are just being used to calc link anchor location

            if (bRecalculateLinks)
            {
                RecalculateLinks(node);
                ' Recalculate all links that link to us
                if (Page IsNot null)
                {
                    For Each nodeOther As MapNode In Page.Nodes
                        For Each link As MapLink In nodeOther.Links.Values
                            If link.sDestination = node.Key Then RecalculateLinks(nodeOther)
                        Next;
                    Next;
                }
                For Each d As DirectionsEnum In New DirectionsEnum() {DirectionsEnum.In, DirectionsEnum.Out}
                    if (node.Links.ContainsKey(d))
                    {
                        private MapNode nodDest = Page.GetNode(node.Links(d).sDestination);
                        If nodDest != null Then RecalculateLinks(nodDest)
                    }
                Next;
            }

            node.Anchors(DirectionsEnum.NorthWest).Points = GetAnchorPoints(node, 0, 0);
            node.Anchors(DirectionsEnum.North).Points = GetAnchorPoints(node, 50, 0);
            node.Anchors(DirectionsEnum.NorthEast).Points = GetAnchorPoints(node, 100, 0);
            node.Anchors(DirectionsEnum.East).Points = GetAnchorPoints(node, 100, 50);
            node.Anchors(DirectionsEnum.SouthEast).Points = GetAnchorPoints(node, 100, 100);
            node.Anchors(DirectionsEnum.South).Points = GetAnchorPoints(node, 50, 100);
            node.Anchors(DirectionsEnum.SouthWest).Points = GetAnchorPoints(node, 0, 100);
            node.Anchors(DirectionsEnum.West).Points = GetAnchorPoints(node, 0, 50);

            if (node Is ActiveNode)
            {
                For Each a As Anchor In node.Anchors.Values
                    a.Visible = true;
                Next;
            }
        }

    }



    ' http://answers.google.com/answers/threadview/id/496030.html
    private static Point Convert3DtoScreen(int x, int y, int z)
    {

        private double Rx = (iOffsetY - 40) / 150 ' rotation about x axis;
        private double Ry = (iOffsetX - 200) / 200 ' rotation about y axis;
        private double Rz = Ry / 5 ' 0 ' rotation about z axis;

        ' First, apply the x-axis rotation to transform coordinates (x, y, z) into coordinates (x0, y0, z0).
        private double x0 = x;
        private double y0 = y * Math.Cos(Rx) + z * Math.Sin(Rx);
        private double z0 = z * Math.Cos(Rx) - y * Math.Sin(Rx);

        ' Then apply the y-axis rotation to (x0, y0, z0) to obtain (x1, y1, z1).
        private double x1 = x0 * Math.Cos(Ry) - z0 * Math.Sin(Ry);
        private double y1 = y0;
        private double z1 = z0 * Math.Cos(Ry) + x0 * Math.Sin(Ry);

        ' Finally, apply the z-axis rotation to obtain the point (x2, y2).
        private double x2 = x1 * Math.Cos(Rz) + y1 * Math.Sin(Rz);
        private double y2 = y1 * Math.Cos(Rz) - x1 * Math.Sin(Rz);

        'Debug.WriteLine(String.Format("(X: {0}, Y: {1}, Z: {2}) -> (X: {3}, Y: {4})", x, y, z, x2, y2))
        'If bDisplay Then
        '    Debug.WriteLine(String.Format("(X: {0}, Y: {1}, Z: {2}) -> (X: {3}, Y: {4}, Z: {5})", x, y, z, x2, y2, z1))
        'End If

        return new Point(CInt(x2), CInt(y2)) ' + 200);

    }


    private Point ConvertScreento3D(Point ptScreen, int zTest)
    {

        ' Calculate a series of coordinates on the 3D given by the screen coordinate, and return the first
        ' point in 3D space where we cross the plane given by z

        ' Could probably vastly improve this function if needed, using a bisection algorithm...
        ' http://en.wikipedia.org/wiki/Bisection_method

        private Double, y As Double, z As Double x;
        private double zUnknown = -2500;

        do
        {
            zUnknown += 1;
            Get3DCoord(ptScreen.X, ptScreen.Y, zUnknown, x, y, z);
        Loop While z < zTest;

        return new Point(CInt(x), CInt(y));

    }


    ' Get the original X and Y coords, assuming we are given the 3D point, plus Z
    private void Get3DCoord(double x2, double y2, double z2, ref x As Double, ref y As Double, ref z As Double)
    {

        private double Rx = (iOffsetY - 40) / 150 ' rotation about x axis;
        private double Ry = (iOffsetX - 200) / 200 ' rotation about y axis;
        private double Rz = Ry / 5 ' 0 ' rotation about z axis;

        ' Using http://www.numberempire.com/equationsolver.php

        'Dim x2 As Double = pt.X
        'Dim y2 As Double = pt.Y - 200
        'Dim z2 As Double = z '?!!

        ' x2 = x1 * Cos(Rz) + y1 * Sin(Rz)  -A
        ' y2 = y1 * Cos(Rz) - x1 * Sin(Rz)  -B
        ' =>
        ' x1 = -(sin(Rz)*y1-x2)/cos(Rz) -fromA -A
        ' y1 = (x2-cos(Rz)*x1)/sin(Rz)  -fromA -B
        ' x1 = -(y2-cos(Rz)*y1)/sin(Rz) -fromB -C
        ' y1 = (y2+sin(Rz)*x1)/cos(Rz)  -fromB -D
        ' =>
        ' y1 = (cos(Rz)*y2+sin(Rz)*x2)/(sin(Rz)^2+cos(Rz)^2)    -fromA,C
        ' x1 = -(sin(Rz)*y2-cos(Rz)*x2)/(sin(Rz)^2+cos(Rz)^2)   -fromB,D
        ' z1 = z2

        private double x1 = -(Math.Sin(Rz) * y2 - Math.Cos(Rz) * x2) / (Math.Sin(Rz) ^ 2 + Math.Cos(Rz) ^ 2);
        private double y1 = (Math.Cos(Rz) * y2 + Math.Sin(Rz) * x2) / (Math.Sin(Rz) ^ 2 + Math.Cos(Rz) ^ 2);
        private double z1 = z2;

        '---

        ' x1 = x0 * Cos(Ry) - z0 * Sin(Ry)  -A
        ' z1 = z0 * Cos(Ry) + x0 * Sin(Ry)  -B
        ' =>
        ' x0 = (sin(Ry)*z0+x1)/cos(Ry)  -fromA -A
        ' z0 = -(x1-cos(Ry)*x0)/sin(Ry) -fromA -B
        ' x0 = (z1-cos(Ry)*z0)/sin(Ry)  -fromB -C
        ' z0 = (z1-sin(Ry)*x0)/cos(Ry)  -fromB -D
        ' =>
        ' z0 = (cos(Ry)*z1-sin(Ry)*x1)/(sin(Ry)^2+cos(Ry)^2)    -fromA,C
        ' x0 = (sin(Ry)*z1+cos(Ry)*x1)/(sin(Ry)^2+cos(Ry)^2)    -fromB,D
        ' y0 = y1

        private double x0 = (Math.Sin(Ry) * z1 + Math.Cos(Ry) * x1) / (Math.Sin(Ry) ^ 2 + Math.Cos(Ry) ^ 2);
        private double y0 = y1;
        private double z0 = (Math.Cos(Ry) * z1 - Math.Sin(Ry) * x1) / (Math.Sin(Ry) ^ 2 + Math.Cos(Ry) ^ 2);

        '---

        ' y0 = y * Cos(Rx) + z * Sin(Rx)    -A
        ' z0 = z * Cos(Rx) - y * Sin(Rx)    -B
        ' =>
        ' y = -(sin(Rx)*z-y0)/cos(Rx)   -fromA -A
        ' z = (y0-cos(Rx)*y)/sin(Rx)    -fromA -B
        ' y = -(z0-cos(Rx)*z)/sin(Rx)   -fromB -C
        ' z = (z0+sin(Rx)*y)/cos(Rx)    -fromB -D
        ' =>
        ' z = (cos(Rx)*z0+sin(Rx)*y0)/(sin(Rx)^2+cos(Rx)^2)     -fromA,C
        ' y = -(sin(Rx)*z0-cos(Rx)*y0)/(sin(Rx)^2+cos(Rx)^2)    -fromB,D
        ' x = x0

        x = x0
        y = -(Math.Sin(Rx) * z0 - Math.Cos(Rx) * y0) / (Math.Sin(Rx) ^ 2 + Math.Cos(Rx) ^ 2)
        z = (Math.Cos(Rx) * z0 + Math.Sin(Rx) * y0) / (Math.Sin(Rx) ^ 2 + Math.Cos(Rx) ^ 2)

        'Debug.WriteLine(String.Format("(X: {0}, Y: {1}) -> (X: {2}, Y: {3}, Z: {4})", x2, y2, x, y, z))

    }


    private Color HotTrackColour(Color colour, byte bAlpha = 0)
    {
        If bAlpha = 0 Then bAlpha = colour.A
        return Color.FromArgb(bAlpha, Math.Max(colour.R - 30, 0), Math.Max(colour.G - 30, 0), Math.Max(colour.B - 30, 0));
    }


    private void DrawAxes(Graphics g)
    {
        'For i As Integer = -5 To 5
        '    g.DrawPolygon(New Pen(Color.Cyan), New Point() {Planes.GetPoint2D(New Point3D(0, 0, i)), Planes.GetPoint2D(New Point3D(5, 0, i)), Planes.GetPoint2D(New Point3D(5, 5, i)), Planes.GetPoint2D(New Point3D(0, 5, i))})
        'Next
        g.DrawLine(New Pen(Color.FromArgb(70, 255, 0, 0), 1), Planes.GetPoint2D(New Point3D(-1000, 0, 0)), Planes.GetPoint2D(New Point3D(1000, 0, 0)));
        g.DrawLine(New Pen(Color.FromArgb(70, 0, 255, 0), 1), Planes.GetPoint2D(New Point3D(0, -1000, 0)), Planes.GetPoint2D(New Point3D(0, 1000, 0)));
        g.DrawLine(New Pen(Color.FromArgb(70, 0, 0, 255), 1), Planes.GetPoint2D(New Point3D(0, 0, -1000)), Planes.GetPoint2D(New Point3D(0, 0, 1000)));
    }
    private void DrawGrid(Graphics g)
    {

        if (ActiveNode IsNot null)
        {
            private int iRange = 200;
            for (int x = -iRange; x <= iRange; x += 2)
            {
                g.DrawLine(New Pen(Color.FromArgb(30, 0, 0, 0)), Planes.GetPoint2D(New Point3D(x, -iRange, ActiveNode.Location.Z)), Planes.GetPoint2D(New Point3D(x, iRange, ActiveNode.Location.Z)));
                g.DrawLine(New Pen(Color.FromArgb(30, 0, 0, 0)), Planes.GetPoint2D(New Point3D(-iRange, x, ActiveNode.Location.Z)), Planes.GetPoint2D(New Point3D(iRange, x, ActiveNode.Location.Z)));
            Next;
        }

    }


    private void DrawNode(Graphics g, MapNode node)
    {

        try
        {
            'Dim penBlack As New Pen(Color.DarkCyan, 1)
            'Dim penJoin As New Pen(Color.DarkBlue, 3)
            'Dim points(3) As Point
#if Runner
            If ! node.Seen Then Exit Sub
            If Adventure.htblLocations(node.Key).HideOnMap Then Exit Sub
#endif

            private int X = node.Location.X * iScale '- iBoundX  ' Left;
            private int Y = node.Location.Y * iScale '- iBoundY  ' Top;
            private int X2 = (X + node.Width * iScale)   ' Right;
            private int Y2 = (Y + node.Height * iScale)  ' Bottom;
            'Dim Z As Integer = node.Z * iScale              ' Level


            private Brush brushNodeText;
            private Brush brushNodeBackground;
            private Pen penBorder;
            private byte Alpha = 255;

            if (SelectedNodes.Contains(node))
            {
                brushNodeBackground = New System.Drawing.SolidBrush(Color.FromArgb(200, NODESELECTED))
                brushNodeText = New System.Drawing.SolidBrush(NODETEXT)
                penBorder = New Pen(NODEBORDER, 1)
                if (node Is HotTrackedNode)
                {
                    brushNodeBackground = New System.Drawing.SolidBrush(HotTrackColour(NODESELECTED))
                }
            ElseIf node == HotTrackedNode Then
                brushNodeBackground = New System.Drawing.SolidBrush(HotTrackColour(NODEBACKGROUND, 200)) ' Color.FromArgb(200, Math.Max(NODEBACKGROUND.R - 30, 0), Math.Max(NODEBACKGROUND.G - 30, 0), Math.Max(NODEBACKGROUND.B - 30, 0)))
                brushNodeText = New System.Drawing.SolidBrush(NODETEXT)
                penBorder = New Pen(NODEBORDER, 1)
            Else
                if (ActiveNode Is null || node.Location.Z = ActiveNode.Location.Z)
                {
                    ' Nodes on same level as hot-tracked node
                    brushNodeBackground = New System.Drawing.SolidBrush(Color.FromArgb(200, NODEBACKGROUND))
                    brushNodeText = New System.Drawing.SolidBrush(NODETEXT)
                    penBorder = New Pen(NODEBORDER, 1)
                Else
                    ' Nodes on a different level to hot-tracked node
                    brushNodeBackground = New System.Drawing.SolidBrush(Color.FromArgb(50, NODEBACKGROUND))
                    brushNodeText = New System.Drawing.SolidBrush(Color.FromArgb(50, NODETEXT))
                    penBorder = New Pen(Color.FromArgb(50, NODEBORDER))
                    Alpha = 50
                }
            }

            If node.Overlapping Then penBorder = Pens.Red
#if Generator
            if (Adventure.htblLocations(node.Key).HideOnMap)
            {
                penBorder = New Pen(penBorder.Color)
                penBorder.DashStyle = DashStyle.DashDot;
            }
#endif
            'Dim loc As clsLocation = Adventure.htblLocations(node.Key)
            'If loc.arlDirections(DirectionsEnum.Down).LocationKey <> "" Then
            '    Dim node2 As MapNode = Page.GetNode(loc.arlDirections(DirectionsEnum.Down).LocationKey)
            '    If node2 IsNot Nothing Then
            '        'g.DrawLine(penBlack, CSng(node.Points(0).X + (node.Points(1).X - node.Points(0).X) / 2), node.Points(0).Y, CSng(node2.Points(0).X + (node2.Points(1).X - node2.Points(0).X) / 2), node2.Points(0).Y)
            '        g.DrawLine(penJoin, CSng(node.Points(0).X + (node.Points(2).X - node.Points(0).X) / 2), CSng(node.Points(0).Y + (node.Points(2).Y - node.Points(0).Y) / 2), CSng(node2.Points(0).X + (node2.Points(2).X - node2.Points(0).X) / 2), CSng(node2.Points(0).Y + (node2.Points(2).Y - node2.Points(0).Y) / 2))
            '    End If
            'End If

            'g.SmoothingMode = SmoothingMode.HighQuality

            '            DrawLinks(g, node, DirectionsEnum.Down)
            for (DirectionsEnum eDir = DirectionsEnum.North; eDir <= DirectionsEnum.NorthWest; eDir++)
            {
                private MapNode nodDest = null;
                if (node.Links.ContainsKey(eDir))
                {
                    nodDest = Map.FindNode(node.Links(eDir).sDestination)
                    if (nodDest IsNot null)
                    {
                        private int iComp = nodDest.CompareTo(node);
                        'If nodDest IsNot Nothing AndAlso (nodDest.Location.Z > node.Location.Z OrElse (nodDest.Location.Z = node.Location.Z AndAlso nodDest.Location.Y < node.Location.Y) OrElse (nodDest Is node AndAlso eDir = DirectionsEnum.Down)) Then
                        if (nodDest IsNot null && (iComp < 0 || (eDir = DirectionsEnum.Down && iComp = 0)))
                        {
                            'If eDir <> DirectionsEnum.Down Then
                            DrawLinks(g, node, eDir);
                        }
                    }
                }
            Next;
#if Runner
            if (Adventure.Player IsNot null && Adventure.Player.HasRouteInDirection(DirectionsEnum.Down, false, node.Key) && Not Adventure.Player.HasSeenLocation(Adventure.htblLocations(node.Key).arlDirections(DirectionsEnum.Down).LocationKey))
            {
                DrawOutArrow(g, node, DirectionsEnum.Down);
            }
#endif
            'brushNodeBackground = New System.Drawing.SolidBrush(Color.FromArgb(200, Color.FromArgb(Math.Min(150 - (node.Location.Z * 2), 255), Math.Min(200 - (node.Location.Z * 2), 255), Math.Min(255 - (node.Location.Z * 2), 255))))

            g.FillPolygon(brushNodeBackground, node.Points);
            g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias;

            ' Create a transformation on the graphics, so we can squish the text correctly into the box
            private System.Drawing.Drawing2D.Matrix transformNone = g.Transform;
            'Dim myMatrix As New Matrix()
            'Dim dfXShear As Single = -CSng((node.Points(0).X - node.Points(3).X) / (node.Points(3).Y - node.Points(0).Y)) '(Y2 - Y)) '
            'Dim dfXScale As Single = -CSng(node.Points(0).X - node.Points(1).X) / (X2 - X)
            'Dim dfYScale As Single = CSng((node.Points(2).Y - node.Points(1).Y) / (Y2 - Y))
            ''myMatrix.Scale(dfXScale, dfYScale)
            'myMatrix.Translate(CSng(node.Points(0).X), CSng(node.Points(0).Y))
            ''myMatrix.Rotate(CInt((-iOffsetX + 200) / 20))
            'If Not Double.IsInfinity(dfXShear) AndAlso Not Double.IsNaN(dfXShear) Then myMatrix.Shear(dfXShear, 0)
            'If dfXScale <> 0 AndAlso dfYScale <> 0 Then myMatrix.Scale(dfXScale, dfYScale)
            'g.MultiplyTransform(myMatrix)
            private New StringFormat sf;
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            private Rectangle rect = new Rectangle(node.Points(0).X, node.Points(0).Y, X2 - X, Y2 - Y);
            private New Rectangle(2, 2, rect.Width - 4, Math.Max(rect.Height - 4, 2)) rectInner;
            'Dim rectInner As New Rectangle(2, 2, rect.Width - 4, rect.Height - 4)

            private string sText = node.Text ' Adventure.htblLocations(node.Key).ShortDescription;
            'If txtRename.Visible AndAlso node Is ActiveNode Then sText &= "_" '"|"
            private Font fntText = GetFont(g, rectInner, sText);
            if (txtRename.Visible && node Is ActiveNode)
            {
                if (txtRename.SelectionLength > 0)
                {
                    sText = sLeft(sText, txtRename.SelectionStart) & "[" & sMid(sText, txtRename.SelectionStart + 1, txtRename.SelectionLength) & "]" & sRight(sText, sText.Length - txtRename.SelectionStart - txtRename.SelectionLength)
                Else
                    sText = sLeft(sText, txtRename.SelectionStart) & "|" & sRight(sText, sText.Length - txtRename.SelectionStart)
                }
            }
            'g.DrawString(sText, fntText, brushNodeText, rectInner, sf)
            'If txtRename.Visible AndAlso txtRename.SelectedText <> "" Then
            '    g.DrawString(txtRename.SelectedText, fntText, Brushes.Chocolate, rectInner, sf)
            'End If

            'g.Transform = transformNone ' Restore transformation back to normal

            'If node.Key = "Location1" OrElse node.Key = "Location2" Then
            'g.DrawEllipse(New Pen(Color.Red, 4), New Rectangle(Planes(node.Location.Z).GetPointOnPlane(0, 0), New Size(5, 5)))
            'g.DrawEllipse(New Pen(Color.Red, 4), New Rectangle(Planes(node.Location.Z).GetPointOnPlane(1000, 0), New Size(5, 5)))
            'g.DrawEllipse(New Pen(Color.Red, 4), New Rectangle(Planes(node.Location.Z).GetPointOnPlane(1000, 1000), New Size(5, 5)))
            'g.DrawString(sText, fntText, Brushes.Red, rectInner, sf)

            If ! Planes.ContainsKey(node.Location.Z) Then Exit Sub ' For now...

            private Matrix myMatrix = CType(Planes(node.Location.Z).matrix.Clone, Matrix);
            myMatrix.Translate(node.Location.X * iScale, node.Location.Y * iScale);
            g.MultiplyTransform(myMatrix);
            'g.DrawString(sText, fntText, Brushes.Green, rectInner, sf)
            'myMatrix.Reset()
            'g.MultiplyTransform(myMatrix)
            g.DrawString(sText, fntText, brushNodeText, rectInner, sf);
            'g.Transform.Reset()
            'g.DrawImage(My.Resources.Resources.imgOut32, New Rectangle(X2 - X, Y, 100, 100))

            private System.Drawing.Drawing2D.Matrix transformNode = g.Transform;

            g.Transform = transformNone ' Restore transformation back to normal;
            'End If

            ' Draw the outer box
            g.DrawPolygon(penBorder, node.Points);

            for (DirectionsEnum eDir = DirectionsEnum.North; eDir <= DirectionsEnum.NorthWest; eDir++)
            {
                private MapNode nodDest = null;
                if (node.Links.ContainsKey(eDir))
                {
                    If node.Links(eDir).sDestination <> "" Then nodDest = Map.FindNode(node.Links(eDir).sDestination)
                    if (Not (node Is nodDest && eDir = DirectionsEnum.Down))
                    {
                        if (node.Links(eDir).sDestination = "" || (nodDest IsNot null && nodDest.CompareTo(node) >= 0))
                        {
                            DrawLinks(g, node, eDir);
                        }
                    }
                }
#if Runner
                if (eDir <> DirectionsEnum.Down)
                {
                    if (Adventure.Player IsNot null && Adventure.Player.HasRouteInDirection(eDir, false, node.Key) && Not Adventure.Player.HasSeenLocation(Adventure.htblLocations(node.Key).arlDirections(eDir).LocationKey))
                    {
                        if (eDir = DirectionsEnum.In || eDir = DirectionsEnum.Out)
                        {
                            DrawInOutIcon(eDir, g, node, , Alpha);
                        Else
                            DrawOutArrow(g, node, eDir);
                        }
                    }
                }
#endif
            Next;


            if ((node.bDrawOut && node.bHasOut) || node.Links.ContainsKey(DirectionsEnum.Out))
            {
#if Runner
                if (Adventure.Player.HasRouteInDirection(DirectionsEnum.Out, false, node.Key))
                {
#endif
                DrawInOutIcon(DirectionsEnum.Out, g, node, , Alpha);
#if Runner
                }
#endif
            }
            if ((node.bDrawIn && node.bHasIn) || node.Links.ContainsKey(DirectionsEnum.In))
            {
#if Runner
                if (Adventure.Player.HasRouteInDirection(DirectionsEnum.In, false, node.Key))
                {
#endif
                DrawInOutIcon(DirectionsEnum.In, g, node, , Alpha);
#if Runner
                }
#endif
            }


#if Generator
            ' Draw any anchors
            if (node.Anchors IsNot null && node.Anchors.Count > 0)
            {
                For Each a As Anchor In node.Anchors.Values
                    If a.Visible Then DrawAnchor(g, a)
                Next;
            }
#endif


            'For eDir As DirectionsEnum = DirectionsEnum.North To DirectionsEnum.NorthWest
            '    If node.Links.ContainsKey(eDir) Then
            '        Dim nodDest As MapNode = Map.FindNode(node.Links(eDir).sDestination)
            '        If nodDest IsNot Nothing Then
            '            If nodDest.Location.Z < node.Location.Z OrElse (nodDest.Location.Z = node.Location.Z AndAlso nodDest.Location.Y >= node.Location.Y) Then
            '                'If eDir <> DirectionsEnum.Down Then
            '                DrawLinks(g, node, eDir)
            '            End If
            '        End If
            '    End If
            'Next

        }
        catch (Exception ex)
        {
            'ErrMsg("Error drawing node", ex)
        }

    }


    private void DrawAnchor(Graphics g, Anchor a)
    {
        private Brush brushAnchor;
        if (a Is HotTrackedAnchor)
        {
            brushAnchor = New SolidBrush(Color.FromArgb(125, 210, 120, 30))
        Else
            brushAnchor = New SolidBrush(Color.FromArgb(100, 255, 150, 50))
        }
        g.FillPolygon(brushAnchor, a.Points);
        g.DrawPolygon(Pens.DarkOrange, a.Points);
    }


    'Private Function GetAnchor(ByVal node As MapNode, ByVal d As DirectionsEnum) As Anchor

    '    If node.Anchors Is Nothing OrElse Not node.Anchors.ContainsKey(d) Then
    '        CreateAnchor(node, d)
    '    End If
    '    Return node.Anchors(d)

    'End Function


    private void DrawOutArrow(Graphics g, MapNode node, DirectionsEnum eDir, Pen pen = null)
    {

        private Pen penLink;
        if (pen Is null)
        {
            penLink = New Pen(Color.FromArgb(30, LINKCOLOUR), CInt(iScale / 5))
            if (ActiveNode Is null || node.Location.Z = ActiveNode.Location.Z)
            {
                penLink = New Pen(Color.FromArgb(100, LINKCOLOUR), CInt(iScale / 5))
            }
            penLink.DashStyle = DashStyle.Solid;
            penLink.StartCap = LineCap.Round;
        Else
            penLink = pen
        }
        private New AdjustableArrowCap(4, 4, True) bigarrow;
        private CustomLineCap cap = bigarrow;
        penLink.EndCap = LineCap.Custom;
        penLink.CustomEndCap = cap;

        private Point ptStart = GetLinkPoint(node, eDir);
        private Point ptEnd = GetBezierAssister(node, eDir, 10 * iScale);

        g.DrawLine(penLink, ptStart, ptEnd);

    }


    private void DrawLinks(Graphics g, MapNode node, DirectionsEnum eDir)
    {

        if (node.Links.ContainsKey(eDir))
        {
            private MapLink link = node.Links(eDir);
            With link;
#if Runner
                If .Style = DashStyle.Dot && ! Adventure.Player.HasRouteInDirection(eDir, false, node.Key) Then Exit Sub
                If .sDestination == null || ! Adventure.htblLocations(.sDestination).SeenBy(Adventure.Player.Key) Then Exit Sub
#endif

                private New Pen(Color.FromArgb(30, LINKCOLOUR), CInt(iScale / 5)) penLink;
                private MapNode nodDest = Page.GetNode(link.sDestination);

                if (ActiveNode Is null || node.Location.Z = ActiveNode.Location.Z || (nodDest IsNot null && nodDest.Location.Z = ActiveNode.Location.Z && nodDest.Seen))
                {
                    ' Nodes on same level as selected node
                    penLink = New Pen(Color.FromArgb(100, LINKCOLOUR), CInt(iScale / 5))
                }

                penLink.DashStyle = .Style;
                penLink.StartCap = LineCap.Flat ' LineCap.Round;

                ' Don't show dotted lines if we haven't been restricted in that direction yet
                if (.Style = DashStyle.Dot)
                {
                    private clsLocation src = Adventure.htblLocations(.sSource);
                    If src.arlDirections(eDir).Restrictions.Count > 0 && ! src.arlDirections(eDir).bEverBeenBlocked Then penLink.DashStyle = DashStyle.Solid
                }

                if (Not .Duplex)
                {
                    ' Draw as an arrow
                    'penLink.EndCap = LineCap.ArrowAnchor
                    private New AdjustableArrowCap(4, 4, True) bigarrow;
                    private CustomLineCap cap = bigarrow;
                    penLink.EndCap = LineCap.Custom;
                    penLink.CustomEndCap = cap;
                Else
                    penLink.EndCap = LineCap.Round ' LineCap.Round;
                }

                if (link Is NewLink)
                {
                    If node.Anchors.ContainsKey(eDir) Then node.Anchors(eDir).Visible = true
                    penLink.Color = Color.FromArgb(100, 255, 0, 0);
                ElseIf link == SelectedLink Then
                    If node.Anchors.ContainsKey(link.eSourceLinkPoint) Then node.Anchors(link.eSourceLinkPoint).Visible = true
                    If nodDest != null && nodDest.Anchors.ContainsKey(link.eDestinationLinkPoint) Then nodDest.Anchors(link.eDestinationLinkPoint).Visible = true
                    penLink.Color = Color.FromArgb(100, LINKSELECTED);
                ElseIf link == HotTrackedLink Then
                    If node.Anchors.ContainsKey(eDir) Then node.Anchors(eDir).Visible = true
                    If nodDest != null && nodDest.Anchors.ContainsKey(link.eDestinationLinkPoint) Then nodDest.Anchors(link.eDestinationLinkPoint).Visible = true
                    penLink.Color = Color.FromArgb(150, HotTrackColour(LINKCOLOUR));
                }

                if (.sDestination = node.Key && eDir <> DirectionsEnum.In && eDir <> DirectionsEnum.Out Then '&& .eDestinationLinkPoint = OppositeDirection(eDir))
                {
                    DrawOutArrow(g, node, eDir, penLink);
                    Exit Sub;
                }

                '#If Runner Then
                '        If Not Adventure.htblLocations(.sDestination).SeenBy(Adventure.Player.Key) Then
                '            DrawOutArrow(g, node, eDir, penLink)
                '            Exit Sub
                '        End If
                '#End If

                'Debug.WriteLine(.Points(0).ToString & .Points(1).ToString & .Points(2).ToString)
                if (.Points IsNot null && Not (.Points.Length = 3 && .Points(2).X = 0 && .Points(2).Y = 0))
                {
                    if (.Points.Length = 3 && .sDestination = "")
                    {
                        g.DrawBezier(penLink, .Points(0), .Points(1), .Points(2), .Points(2));
                    ElseIf .Points.Length = 4 && (.OrigMidPoints.Length = 0 || link == NewLink) Then
                        g.DrawBezier(penLink, .Points(0), .Points(1), .Points(2), .Points(3));
                    Else
                        g.DrawCurve(penLink, .Points) ', 0.5F);
                    }
                }

                if (link Is SelectedLink || link Is NewLink || link Is HotTrackedLink)
                {
                    if (link.Anchors.Count > 0)
                    {
#if False
                            private Point p = .Points(1);
                            g.DrawEllipse(Pens.Red, New Rectangle(p.X - 2, p.Y - 2, 5, 5));
                            p = .Points(.Points.Length - 2)
                            g.DrawEllipse(Pens.Red, New Rectangle(p.X - 2, p.Y - 2, 5, 5));
#endif
                        For Each a As Anchor In link.Anchors
                            DrawAnchor(g, a);
                        Next;
                        'Else
                        'For i As Integer = 1 To .Points.Length - 2
                        '    Dim p As Point = .Points(i)
                        '    g.DrawEllipse(Pens.Red, New Rectangle(p.X - 2, p.Y - 2, 5, 5))
                        'Next
                    }
                }

                ' Draw the destination In/Out icon if the destination node is less significant than us
                if (nodDest IsNot null && (eDir = DirectionsEnum.Out || eDir = DirectionsEnum.In))
                {
                    private byte Alpha = 255;
                    If ! SelectedNodes.Contains(node) && ! (ActiveNode == null || node.Location.Z = ActiveNode.Location.Z) Then Alpha = 50
                    DrawInOutIcon(OppositeDirection(eDir), g, nodDest, , Alpha);
                }

            }

        }

    }


    private void DrawInOutIcon(DirectionsEnum eDir, Graphics g, MapNode node, MapNode nodDest = null, byte Alpha = 255)
    {

        try
        {

            'If Not node.Links.ContainsKey(eDir) Then Exit Sub
            If (eDir = DirectionsEnum.In && ! node.bHasIn) || (eDir = DirectionsEnum.Out && ! node.bHasOut) Then Exit Sub

            private System.Drawing.Drawing2D.Matrix transformNone = g.Transform;
            private int iCircleWidth = CInt(iScale / 2);
            private Point ptInOut;

            For Each n As MapNode In New MapNode() {node, nodDest}
                if (n IsNot null)
                {
                    private DirectionsEnum eInOut = eDir;
                    If n != node Then eInOut = OppositeDirection(eInOut)

                    if (n Is node || node.CompareTo(nodDest) > 0)
                    {
                        if ((n.bHasOut && eInOut = DirectionsEnum.Out) || (n.bHasIn && eInOut = DirectionsEnum.In))
                        {
                            ptInOut = CType(IIf(eInOut = DirectionsEnum.In, n.ptIn, n.ptOut), Point)

                            private New Rectangle(ptInOut.X - iCircleWidth, ptInOut.Y - iCircleWidth, iCircleWidth * 2, iCircleWidth * 2) rectInOut;
                            private string sInOut;

                            If ! Planes.ContainsKey(n.Location.Z) Then Exit Sub

                            private Matrix myMatrix = CType(Planes(n.Location.Z).matrix.Clone, Matrix);
                            myMatrix.Translate(n.Location.X * iScale, n.Location.Y * iScale);
                            g.MultiplyTransform(myMatrix);

                            private Brush brushBackground;
                            private Pen penBorder;

                            if (eInOut = DirectionsEnum.In)
                            {
                                sInOut = "IN"
                                brushBackground = New System.Drawing.SolidBrush(HotTrackColour(Color.LightGreen, Alpha))
                                penBorder = New Pen(HotTrackColour(Color.DarkGreen, Alpha))
                            Else
                                sInOut = "OUT"
                                brushBackground = New System.Drawing.SolidBrush(HotTrackColour(Color.Pink, Alpha))
                                penBorder = New Pen(HotTrackColour(Color.DarkRed, Alpha))
                            }
                            g.FillEllipse(brushBackground, rectInOut);
                            g.DrawEllipse(penBorder, rectInOut);
                            private Font fntText2 = GetFont(g, rectInOut, sInOut);
                            private New StringFormat sf;
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Center;
                            g.DrawString(sInOut, fntText2, New System.Drawing.SolidBrush(HotTrackColour(Color.Black, Alpha)), New Rectangle(rectInOut.X, rectInOut.Y, rectInOut.Width, CInt(rectInOut.Height * 1.08)), sf);

                            g.Transform = transformNone;
                        }
                    }
                }
            Next;

        }
        catch (Exception ex)
        {

        }

    }


    private Point GetBezierAssister(MapNode node, DirectionsEnum d, int iDist)
    {

        ' Make this a function of the distance between the two node points

        private Point pt;

        If iDist = 0 Then iDist = 1
        private int iOffsetX = 0;
        private int iOffsetY = 0;


        if (node IsNot null)
        {
            iOffsetX = CInt(iDist * 40 / iScale / node.Width)
            iOffsetY = CInt(iDist * 40 / iScale / node.Height)
        }

        switch (d)
        {
            case DirectionsEnum.North:
                {
                pt = GetRelativePoint(node, 50, -iOffsetY) ' 50,-50
            case DirectionsEnum.NorthEast:
                {
                pt = GetRelativePoint(node, 100 + CInt(3 * iOffsetX / 4), -CInt(iOffsetY / 2)) ' 150,-50
            case DirectionsEnum.East:
                {
                pt = GetRelativePoint(node, 100 + iOffsetX, 50) ' 150,50
            case DirectionsEnum.SouthEast:
                {
                pt = GetRelativePoint(node, 100 + CInt(3 * iOffsetX / 4), 100 + CInt(iOffsetY / 2)) '150,150
            case DirectionsEnum.South:
                {
                pt = GetRelativePoint(node, 50, 100 + iOffsetY) ' 50,150
            case DirectionsEnum.SouthWest:
                {
                pt = GetRelativePoint(node, -CInt(3 * iOffsetX / 4), 100 + CInt(iOffsetY / 2)) ' -50,150
            case DirectionsEnum.West:
                {
                pt = GetRelativePoint(node, -iOffsetX, 50) ' -50,50
            case DirectionsEnum.NorthWest:
                {
                pt = GetRelativePoint(node, -CInt(3 * iOffsetX / 4), -CInt(iOffsetY / 2)) ' -50,-50
            case DirectionsEnum.Up:
                {
                pt = node.ptUp ' GetRelativePoint(node, 50, 50) ' 50,50
            case DirectionsEnum.Down:
                {
                pt = node.ptDown ' GetRelativePoint(node, 50, 50) ' 50,50
            case DirectionsEnum.In:
                {
                switch (node.eInEdge)
                {
                    case DirectionsEnum.North:
                        {
                        pt = GetRelativePoint(node, 25, -iOffsetY)
                    case DirectionsEnum.South:
                        {
                        pt = GetRelativePoint(node, 75, 100 + iOffsetY)
                    case DirectionsEnum.East:
                        {
                        pt = GetRelativePoint(node, 100 + iOffsetX, 25)
                    case DirectionsEnum.West:
                        {
                        pt = GetRelativePoint(node, -iOffsetX, 75)
                }
            case DirectionsEnum.Out:
                {
                switch (node.eOutEdge)
                {
                    case DirectionsEnum.North:
                        {
                        pt = GetRelativePoint(node, 75, -iOffsetY)
                    case DirectionsEnum.South:
                        {
                        pt = GetRelativePoint(node, 25, 100 + iOffsetY)
                    case DirectionsEnum.East:
                        {
                        pt = GetRelativePoint(node, 100 + iOffsetX, 75)
                    case DirectionsEnum.West:
                        {
                        pt = GetRelativePoint(node, -iOffsetX, 25)
                }
        }

        return pt;

    }


    ' x and y are percentages of the node, so -10, -10 is above and to left of points(0), 100, 100 is points(2)
    private Point GetRelativePoint(MapNode node, double xP, double yP)
    {

        'Dim iWidth As Integer = node.Points(1).X - node.Points(0).X
        'Dim iHeight As Integer = node.Points(3).Y - node.Points(0).Y
        'Dim iSkew As Integer = node.Points(0).X - node.Points(3).X

        'Dim iX As Integer = CInt(iWidth * xP / 100)
        'Dim iY As Integer = CInt(iHeight * yP / 100)

        ''Return New Point(CInt(x + (iSkew / 12)), CInt(y))
        'Return New Point(CInt(node.Points(0).X + iX - (iSkew * yP / 100)), CInt(node.Points(0).Y + iY))

        return Planes.GetPoint2D(node.Location.X + (node.Width * xP / 100), node.Location.Y + (node.Height * yP / 100), node.Location.Z);

    }



    private Point[] GetAnchorPoints(MapNode node, int xP, int yP)
    {

        Dim pointsAnchor(3) As Point

        pointsAnchor(0) = GetRelativePoint(node, xP - (36 / node.Width), yP - (36 / node.Height));
        pointsAnchor(1) = GetRelativePoint(node, xP + (36 / node.Width), yP - (36 / node.Height));
        pointsAnchor(2) = GetRelativePoint(node, xP + (36 / node.Width), yP + (36 / node.Height));
        pointsAnchor(3) = GetRelativePoint(node, xP - (36 / node.Width), yP + (36 / node.Height));

        return pointsAnchor;

    }



    private Font GetFont(Graphics g, RectangleF rectF, string sText)
    {

        private float emSize = Math.Min(CSng(rectF.Height / 3), 36);
        private Font fnt;
        private New StringFormat sf;
        private string[] sWords = sText.Split(" "c);
        private int iWordCount = sWords.Length;

        sf.Alignment = StringAlignment.Center;
        sf.LineAlignment = StringAlignment.Center;

        If emSize < 1 Then emSize = 1
        do
        {
            fnt = New Font("Arial", emSize, FontStyle.Regular, GraphicsUnit.Pixel) ', FontStyle.Regular, GraphicsUnit.Point) ' GraphicsUnit.Pixel)

            'Dim size As SizeF = g.MeasureString(sText, fnt, CInt(rectF.Width), sf)
            private int cf = 0;
            private int lf = 0;
            private bool bOk = false;

            ' Check the whole text fits in our rectangle
            private SizeF size = g.MeasureString(sText, fnt, new SizeF(rectF.Width, Integer.MaxValue), sf, cf, lf);

            if (size.Height <= rectF.Height)
            {
                ' If it does, check each word individually fits in ok
                bOk = True
                For Each sWord As String In sWords
                    g.MeasureString(sWord, fnt, New SizeF(rectF.Width, Integer.MaxValue), sf, cf, lf);
                    if (lf > 1)
                    {
                        bOk = False
                        Exit For;
                    }
                Next;
            }

            ' emSize <= 1 OrElse (size.Height <= rectF.Height AndAlso Not (Not sText.Contains(" ") AndAlso size.Height > emSize * 2)) AndAlso lf <= iWordCount AndAlso cf >= iMaxWord Then Return fnt
            If bOk || emSize <= 1 Then Return fnt
            emSize -= 1;

        Loop;

    }


    private void tabsMap_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {

#if Generator
        if (e.Button = Windows.Forms.MouseButtons.Right then '&& tabsMap.SelectedTab IsNot null && tabsMap.SelectedTab Is tabsMap.HotTrackedTab)
        {
            cmsTabs.Show(imgMap, imgMap.PointToClient(MousePosition));
        }
#endif

    }



    '#If Not www Then
#if Mono OrElse www
    private void tabsMap_TabIndexChanged(object sender, System.EventArgs e)
    {
        imgMap.Parent = tabsMap.SelectedTab;
#else
    private void tabsMap_TabIndexChanged(object sender, System.EventArgs e)
    {
#endif
        If bLoading Then Exit Sub
        if (Page IsNot null)
        {
            'For Each node As MapNode In Page.Nodes
            '    If node.Anchors.Count > 0 Then
            '        For Each a As Anchor In node.Anchors
            '            a.Visible = False
            '        Next
            '    End If
            'Next
            if (tabsMap.SelectedTab IsNot null)
            {
#if Mono OrElse www
                if (Map.Pages.ContainsKey(SafeInt(tabsMap.SelectedTab.Name)))
                {
                    Page = Map.Pages(SafeInt(tabsMap.SelectedTab.Name))
#else
                if (Map.Pages.ContainsKey(SafeInt(tabsMap.SelectedTab.Key)))
                {
                    Page = Map.Pages(SafeInt(tabsMap.SelectedTab.Key))
#endif
                    RecalculateNodes();
                    CentreMap();
                    RecalculateNodes();
                    ActiveNode = Nothing
                    if (Not bLoading && tabsMap.SelectedTab IsNot null)
                    {
#if Mono OrElse www
                        Map.SelectedPage = tabsMap.SelectedTab.Name;
#else
                        Map.SelectedPage = tabsMap.SelectedTab.Key;
#endif
                    }
                }
            }
        }
    }
    '#End If

    private void btnPlanView_Click(System.Object sender, System.EventArgs e)
    {
        PlanView();
    }


    public void PlanView()
    {
        iOffsetX = 200
        iOffsetY = 40
        RecalculateNodes();
        imgMap.Refresh();
    }


    private Point ptStart;
    private Point ptStartLasso;
    private Point ptEndLasso;
    private int iOffsetStartX = 0;
    private int iOffsetStartY = 0;
    private int iBoundStartX = 0;
    private int iBoundStartY = 0;
    private void imgMap_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        ptStart = e.Location
        iOffsetStartX = iOffsetX
        iOffsetStartY = iOffsetY
        iBoundStartX = iBoundX
        iBoundStartY = iBoundY

        if ((System.Windows.Forms.Control.ModifierKeys And Keys.Shift) > 0)
        {
            ptStartLasso = e.Location
            ActiveNode = Nothing
            SelectedNodes.Clear();
        }

        bDragged = False

        imgMap_MouseMove(sender, e) ' So any hot-tracks are updated;

        if (HotTrackedAnchor IsNot null && SelectedLink IsNot null)
        {
            'If e.Button = Windows.Forms.MouseButtons.Right Then cmsLink.Show(imgMap, imgMap.PointToClient(MousePosition))
        ElseIf HotTrackedNode != null Then
            private bool bSelectedContainsNew = SelectedNodes.Contains(HotTrackedNode);
#if Generator
            if ((Control.ModifierKeys And Keys.Control) > 0)
            {
                if (bSelectedContainsNew)
                {
                    SelectedNodes.Remove(HotTrackedNode);
                Else
                    If ActiveNode == null Then ActiveNode = HotTrackedNode
                    SelectedNodes.Add(HotTrackedNode);
                }
            Else
                ActiveNode = HotTrackedNode
                if (Not bSelectedContainsNew)
                {
                    SelectedNodes.Clear();
                    SelectedNodes.Add(HotTrackedNode);
                }
            }

            If e.Button = Windows.Forms.MouseButtons.Right Then cmsNode.Show(imgMap, imgMap.PointToClient(MousePosition))
#else
            Adventure.Player.WalkTo = HotTrackedNode.Key;
            Adventure.Player.DoWalk();
#endif

        ElseIf HotTrackedAnchor == null Then
            If e.Button = Windows.Forms.MouseButtons.Left && (System.Windows.Forms.Control.ModifierKeys + Keys.Shift) = 0 Then imgMap.Cursor = (Cursor)(Cursors.NoMove2D)
            If e.Button = Windows.Forms.MouseButtons.Right Then imgMap.Cursor = (Cursor)(Cursors.SizeAll)
        }

    }


    public void SelectNode(string sKey, bool bClearSelectionFirst = true)
    {

        If Map == null Then Exit Sub

        private bool bFoundNode = false;
        If bClearSelectionFirst Then SelectedNodes.Clear()
        For Each p As MapPage In Map.Pages.Values
            if (p.GetNode(sKey) IsNot null)
            {
#if Mono OrElse www
                if (tabsMap.TabPages.ContainsKey(p.iKey.ToString))
                {
                    private int iCurrent = Page.iKey;
                    tabsMap.SelectedTab = tabsMap.TabPages(p.iKey.ToString);
#if www
                    If p.iKey <> iCurrent Then tabsMap_TabIndexChanged(null, null)
#endif
                Else
                    tabsMap.TabPages.Add(p.iKey.ToString, p.Label);
                    imgMap.Parent = tabsMap.TabPages(p.iKey.ToString);
                    tabsMap.SelectedTab = tabsMap.TabPages(p.iKey.ToString);
                }
                fRunner.txtInput.Focus();
#else
                tabsMap.SelectedTab = tabsMap.Tabs(p.iKey.ToString);
#endif
                'Application.DoEvents() ' This causes delay between activating and selecting which looks naff
                ActiveNode = Page.GetNode(sKey)
                bFoundNode = True
            }
        Next;
        If bClearSelectionFirst && ! bFoundNode Then ActiveNode = null

#if Runner
        if (LockMapCentre)
        {
            CentreMap();
            CentreAxis();
        ElseIf LockPlayerCentre && SelectedNodes.Count > 0 Then
            CentreOnNode(SelectedNodes(0));
            'RecalculateNodes()
            CentreAxis();
        }
#endif

        imgMap.Refresh();
    }


    private Point3D MouseTo3DCoords(int Z)
    {

        private Point ptMouse = imgMap.PointToClient(MousePosition);
        private int iX = ptMouse.X + iBoundX;
        private int iY = ptMouse.Y + iBoundY;
        private Point pt = ConvertScreento3D(new Point(iX, iY), Z * iScale);

        return new Point3D(CInt(pt.X / iScale), CInt(pt.Y / iScale), Z);

    }


    private void imgMap_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
    {

        private bool bMoved = Math.Abs(e.X - ptStart.X) > 3 || Math.Abs(e.Y - ptStart.Y) > 3;

#if Generator
        if ((e.Button = Windows.Forms.MouseButtons.Left && (Control.ModifierKeys And Keys.Shift) > 0) || e.Button = Windows.Forms.MouseButtons.Middle || (e.Button = Windows.Forms.MouseButtons.Left && e.Button = Windows.Forms.MouseButtons.Right))
        {
            ptEndLasso = e.Location
            imgMap.Cursor = Cursors.Arrow;
            private int x1 = Math.Min(ptStartLasso.X, ptEndLasso.X);
            private int y1 = Math.Min(ptStartLasso.Y, ptEndLasso.Y);
            private int x2 = Math.Max(ptStartLasso.X, ptEndLasso.X);
            private int y2 = Math.Max(ptStartLasso.Y, ptEndLasso.Y);

            private New Generic.List<MapNode> nodes;
            For Each node As MapNode In Page.Nodes
                For Each pt As Point In node.Points
                    if (pt.X >= x1 && pt.X <= x2 && pt.Y >= y1 && pt.Y <= y2)
                    {
                        nodes.Add(node);
                        Exit For;
                    }
                Next;
            Next;

            private bool bRequiresRefresh = false;
            for (int i = SelectedNodes.Count - 1; i <= 0; i += -1)
            {
                if (Not nodes.Contains(SelectedNodes(i)))
                {
                    SelectedNodes.RemoveAt(i);
                    bRequiresRefresh = True
                }
            Next;
            For Each node As MapNode In nodes
                if (Not SelectedNodes.Contains(node))
                {
                    SelectedNodes.Add(node, false);
                    bRequiresRefresh = True
                }
            Next;

            If bRequiresRefresh = true Then imgMap.Refresh()
        }
#endif

        if (e.Button = Windows.Forms.MouseButtons.Left && bMoved)
        {
#if Generator

            bDragged = True

            if (HotTrackedAnchor IsNot null)
            {
                if (e.X <> ptStart.X || e.Y <> ptStart.Y)
                {

                    if (TypeOf HotTrackedAnchor.Parent Is MapLink)
                    {
                        ' Move link anchors
                        private MapLink link = CType(HotTrackedAnchor.Parent, MapLink);
                        for (int i = 0; i <= link.Anchors.Count - 1; i++)
                        {
                            if (link.Anchors(i) Is HotTrackedAnchor)
                            {
                                private Point3D pt = MouseTo3DCoords(link.OrigMidPoints(i).Z);
                                link.OrigMidPoints(i) = pt;
                                Debug.WriteLine("Dragging link anchor " + i);
                            }
                        Next;
                        RecalculateLinks(Page.GetNode(link.sSource));
                        imgMap.Refresh();

                    ElseIf TypeOf HotTrackedAnchor.Parent == MapNode Then
                        ' Resize the node
                        if (ActiveNode IsNot null)
                        {
                            private Integer, iYOffset As Integer, iWidthOffset As Integer, iHeightOffset As Integer iXOffset;

                            private Point3D pt = MouseTo3DCoords(ActiveNode.Location.Z);
                            private MapNode node = CType(HotTrackedAnchor.Parent, MapNode);
                            switch (true)
                            {
                                case HotTrackedAnchor Is node.Anchors(DirectionsEnum.NorthWest):
                                    {
                                    'If node.Height + node.Location.Y - pt.Y > 0 Then
                                    '    ActiveNode.Height = node.Height + node.Location.Y - pt.Y
                                    '    ActiveNode.Location.Y = pt.Y
                                    'End If
                                    'If node.Width + node.Location.X - pt.X > 0 Then
                                    '    ActiveNode.Width = node.Width + node.Location.X - pt.X
                                    '    ActiveNode.Location.X = pt.X
                                    'End If
                                    iHeightOffset = node.Height + node.Location.Y - pt.Y - ActiveNode.Height
                                    iYOffset = pt.Y - ActiveNode.Location.Y
                                    iWidthOffset = node.Width + node.Location.X - pt.X - ActiveNode.Width
                                    iXOffset = pt.X - ActiveNode.Location.X
                                case HotTrackedAnchor Is node.Anchors(DirectionsEnum.North):
                                    {
                                    if (node.Height + node.Location.Y - pt.Y > 0)
                                    {
                                        'ActiveNode.Height = node.Height + node.Location.Y - pt.Y
                                        'ActiveNode.Location.Y = pt.Y
                                        iHeightOffset = node.Height + node.Location.Y - pt.Y - ActiveNode.Height
                                        iYOffset = pt.Y - ActiveNode.Location.Y
                                    }
                                case HotTrackedAnchor Is node.Anchors(DirectionsEnum.NorthEast):
                                    {
                                    'If node.Height + node.Location.Y - pt.Y > 0 Then
                                    '    ActiveNode.Height = node.Height + node.Location.Y - pt.Y
                                    '    ActiveNode.Location.Y = pt.Y
                                    'End If
                                    'ActiveNode.Width = Math.Max(pt.X - node.Location.X, 1)
                                    iHeightOffset = node.Height + node.Location.Y - pt.Y - ActiveNode.Height
                                    iYOffset = pt.Y - ActiveNode.Location.Y
                                    iWidthOffset = Math.Max(pt.X - node.Location.X, 1) - ActiveNode.Width
                                case HotTrackedAnchor Is node.Anchors(DirectionsEnum.East):
                                    {
                                    'ActiveNode.Width = Math.Max(pt.X - node.Location.X, 1)
                                    iWidthOffset = Math.Max(pt.X - node.Location.X, 1) - ActiveNode.Width
                                case HotTrackedAnchor Is node.Anchors(DirectionsEnum.SouthEast):
                                    {
                                    'ActiveNode.Width = Math.Max(pt.X - node.Location.X, 1)
                                    'ActiveNode.Height = Math.Max(pt.Y - node.Location.Y, 1)
                                    iWidthOffset = Math.Max(pt.X - node.Location.X, 1) - ActiveNode.Width
                                    iHeightOffset = Math.Max(pt.Y - node.Location.Y, 1) - ActiveNode.Height
                                case HotTrackedAnchor Is node.Anchors(DirectionsEnum.South):
                                    {
                                    'ActiveNode.Height = Math.Max(pt.Y - node.Location.Y, 1)
                                    iHeightOffset = Math.Max(pt.Y - node.Location.Y, 1) - ActiveNode.Height
                                case HotTrackedAnchor Is node.Anchors(DirectionsEnum.SouthWest):
                                    {
                                    'If node.Width + node.Location.X - pt.X > 0 Then
                                    '    ActiveNode.Width = node.Width + node.Location.X - pt.X
                                    '    ActiveNode.Location.X = pt.X
                                    'End If
                                    'ActiveNode.Height = Math.Max(pt.Y - node.Location.Y, 1)
                                    iHeightOffset = Math.Max(pt.Y - node.Location.Y, 1) - ActiveNode.Height
                                    iWidthOffset = node.Width + node.Location.X - pt.X - ActiveNode.Width
                                    iXOffset = pt.X - ActiveNode.Location.X
                                case HotTrackedAnchor Is node.Anchors(DirectionsEnum.West):
                                    {
                                    if (node.Width + node.Location.X - pt.X > 0)
                                    {
                                        'ActiveNode.Width = node.Width + node.Location.X - pt.X
                                        'ActiveNode.Location.X = pt.X
                                        iWidthOffset = node.Width + node.Location.X - pt.X - ActiveNode.Width
                                        iXOffset = pt.X - ActiveNode.Location.X
                                    }
                            }

                            For Each nodeSel As MapNode In SelectedNodes
                                if (nodeSel.Height + iHeightOffset > 0 && nodeSel.Width + iWidthOffset > 0)
                                {
                                    nodeSel.Height = nodeSel.Height + iHeightOffset;
                                    nodeSel.Width = nodeSel.Width + iWidthOffset;
                                    nodeSel.Location.X += iXOffset;
                                    nodeSel.Location.Y += iYOffset;
                                }
                            Next;

                            For Each nodeSel As MapNode In SelectedNodes
                                RecalculateNode(nodeSel);
                            Next;

                            Page.CheckForOverlaps();
                            imgMap.Refresh();
                        }
                    }
                }
            ElseIf bMoved && HotTrackedNode != null && HotTrackedNode == ActiveNode Then
                ' Move the selected node
                private Point3D pt = MouseTo3DCoords(ActiveNode.Location.Z);
                private int iXOffset = pt.X - CInt(ActiveNode.Width / 2) - ActiveNode.Location.X;
                private int iYOffset = pt.Y - CInt(ActiveNode.Height / 2) - ActiveNode.Location.Y;

                For Each node As MapNode In SelectedNodes
                    node.Location.X += iXOffset;
                    node.Location.Y += iYOffset;
                Next;

                Page.Nodes.Sort();
                For Each node As MapNode In SelectedNodes
                    RecalculateNode(node);
                Next;

                Page.CheckForOverlaps();
                imgMap.Refresh();
            ElseIf (Control.ModifierKeys + Keys.Shift) = 0 Then
#endif

            ' Rotate the display
            imgMap.Cursor = (Cursor)(Cursors.NoMove2D);

            iOffsetX = Math.Max(Math.Min(iOffsetStartX + e.Location.X - ptStart.X, 400), 0)
                iOffsetY = Math.Max(Math.Min(iOffsetStartY - e.Location.Y + ptStart.Y, 250), 0)
                RecalculateNodes();
#if Generator
            }
#endif

            imgMap.Refresh();
        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
            ' Move the whole map
            imgMap.Cursor = (Cursor)(Cursors.SizeAll);
            iBoundX = iBoundStartX - e.Location.X + ptStart.X
            iBoundY = iBoundStartY - e.Location.Y + ptStart.Y
            RecalculateNodes();
            imgMap.Refresh();
            'CentreAxis()
        Else
            private bool bRequiresRefresh = false;

            ' Work out what is under the mouse
            if (e.Button = Windows.Forms.MouseButtons.None)
            {
                private MapNode nodeHotTrack = null;
                private Anchor anchMouse = null;
                private Point ptMouse = imgMap.PointToClient(MousePosition);

#if Generator
                if (Page IsNot null)
                {
                    For Each node As MapNode In Page.Nodes
                        For Each a As Anchor In node.Anchors.Values
                            if (PolygonContainsPoint(a.Points, ptMouse))
                            {
                                anchMouse = a
                                ' Display any anchor the mouse is over
                                a.Visible = true;
                            Else
                                if (Not node Is ActiveNode)
                                {
                                    ' Unless the anchor is on the selected or new link, then hide it
                                    private bool bHideAnchor = true;
                                    For Each linkTest As MapLink In New MapLink() {NewLink, SelectedLink, HotTrackedLink}
                                        if (linkTest IsNot null)
                                        {
                                            private MapNode nodParent = CType(a.Parent, MapNode);
                                            If (linkTest.sSource = nodParent.Key && linkTest.eSourceLinkPoint = a.Direction) _
                                            || (linkTest.sDestination = nodParent.Key && linkTest.eDestinationLinkPoint = a.Direction) Then;
                                                bHideAnchor = False
                                            }
                                        }
                                    Next;
                                    if (a.Visible = bHideAnchor)
                                    {
                                        a.Visible = ! bHideAnchor;
                                        bRequiresRefresh = True
                                    }
                                }
                            }
                        Next;
                    Next;
                }
                if (SelectedLink IsNot null)
                {
                    For Each a As Anchor In SelectedLink.Anchors
                        if (PolygonContainsPoint(a.Points, ptMouse))
                        {
                            anchMouse = a
                            Exit For;
                        }
                    Next;
                }

                If HotTrackedAnchor != anchMouse Then bRequiresRefresh = true
                if (anchMouse IsNot null && anchMouse.Visible)
                {
                    HotTrackedAnchor = anchMouse
                Else
                    HotTrackedAnchor = Nothing
                }

                private MapLink theHotTrackLink = null;
                if (HotTrackedAnchor IsNot null)
                {

                    if (TypeOf HotTrackedAnchor.Parent Is MapNode)
                    {
                        private MapNode parNode = CType(HotTrackedAnchor.Parent, MapNode);
                        ' Select any link attached to this anchor
                        if (parNode.Links.ContainsKey(HotTrackedAnchor.Direction))
                        {
                            theHotTrackLink = parNode.Links(HotTrackedAnchor.Direction)
                        Else
                            ' Look for a link terminating here
                            For Each node As MapNode In Page.Nodes
                                For Each linkOther As MapLink In node.Links.Values
                                    if (linkOther.sDestination = parNode.Key && linkOther.eDestinationLinkPoint = HotTrackedAnchor.Direction)
                                    {
                                        theHotTrackLink = linkOther
                                        Exit For;
                                    }
                                Next;
                            Next;
                        }
                    }
                }
                HotTrackedLink = theHotTrackLink

                ' Only allow node hot tracking if we aren't tracked over an anchor
                if (anchMouse Is null && Page IsNot null)
                {
                    for (int iNode = Page.Nodes.Count - 1; iNode <= 0; iNode += -1)
                    {
                        if (PolygonContainsPoint(Page.Nodes(iNode).Points, ptMouse))
                        {
                            nodeHotTrack = Page.Nodes(iNode)
                            Exit For;
                        }
                    Next;
                }
                HotTrackedNode = nodeHotTrack

                if (HotTrackedNode IsNot null)
                {
                    if (HotTrackedNode Is ActiveNode)
                    {
                        imgMap.Cursor = Cursors.SizeAll;
                    Else
                        imgMap.Cursor = Cursors.Hand;
                    }
                Else
                    imgMap.Cursor = Cursors.Arrow;
                }

                if (HotTrackedAnchor IsNot null)
                {
                    if (TypeOf HotTrackedAnchor.Parent Is MapLink)
                    {
                        imgMap.Cursor = Cursors.SizeAll;
                    }
                    if (NewLink Is null && HotTrackedAnchor.Parent Is ActiveNode)
                    {
                        switch (HotTrackedAnchor.Direction)
                        {
                            case DirectionsEnum.North:
                            case DirectionsEnum.South:
                                {
                                imgMap.Cursor = Cursors.SizeNS;
                            case DirectionsEnum.East:
                            case DirectionsEnum.West:
                                {
                                imgMap.Cursor = Cursors.SizeWE;
                            case DirectionsEnum.NorthEast:
                            case DirectionsEnum.SouthWest:
                                {
                                imgMap.Cursor = Cursors.SizeNESW;
                            case DirectionsEnum.NorthWest:
                            case DirectionsEnum.SouthEast:
                                {
                                imgMap.Cursor = Cursors.SizeNWSE;
                        }
                    }
                }

                if (NewLink IsNot null && Page IsNot null)
                {
                    private MapNode nodStart = Page.GetNode(NewLink.sSource);
                    if (nodStart IsNot null)
                    {
                        private Point3D ptEnd = MouseTo3DCoords(nodStart.Location.Z);
                        ReDim NewLink.OrigMidPoints(0);
                        NewLink.OrigMidPoints(0) = ptEnd;
                        NewLink.Duplex = true;
                        private bool bFoundAnchor = false;
                        For Each nodDest As MapNode In Page.Nodes
                            For Each a As Anchor In nodDest.Anchors.Values
                                if (PolygonContainsPoint(a.Points, ptMouse))
                                {
                                    a.Visible = true;
                                    NewLink.sDestination = (MapNode)(a.Parent).Key;
                                    NewLink.eDestinationLinkPoint = a.Direction;
                                    if (Adventure.htblLocations(NewLink.sDestination).arlDirections(a.Direction).LocationKey <> "")
                                    {
                                        NewLink.Duplex = false;
                                    }
                                    bFoundAnchor = True
                                    Exit For;
                                Else
                                    ' Allow start anchor
                                    If ! (nodStart == nodDest && NewLink.eSourceLinkPoint = a.Direction) Then a.Visible = false
                                }
                            Next;
                            If bFoundAnchor Then Exit For
                        Next;
                        if (Not bFoundAnchor)
                        {
                            NewLink.sDestination = "";
                        }
                        RecalculateLinks(nodStart);

                        bRequiresRefresh = True
                    }
                }
#else
                ' Only allow node hot tracking if we aren't tracked over an anchor
                if (anchMouse Is null && Page IsNot null)
                {
                    for (int iNode = Page.Nodes.Count - 1; iNode <= 0; iNode += -1)
                    {
                        if (PolygonContainsPoint(Page.Nodes(iNode).Points, ptMouse))
                        {
                            if (Page.Nodes(iNode).Seen)
                            {
                                nodeHotTrack = Page.Nodes(iNode)
                                Exit For;
                            }
                        }
                    Next;
                }
                HotTrackedNode = nodeHotTrack

                if (HotTrackedNode IsNot null)
                {
                    imgMap.Cursor = (Cursor)(Cursors.Hand);
                Else
                    imgMap.Cursor = (Cursor)(Cursors.Arrow);
                }
#endif

                if (bRequiresRefresh)
                {
                    imgMap.Refresh();
                    bRequiresRefresh = False
                }

            }
        }

    }


    private void CentreOnPoint(Point3D point, bool bCentreOnMouse = false)
    {

        private Point pt = TranslateToScreen(point);
        private Point ptTarget;

        if (bCentreOnMouse)
        {
            ptTarget = imgMap.PointToClient(MousePosition)
        Else
            ptTarget = New Point(CInt(sizeImage.Width / 2), CInt(sizeImage.Height / 2))
        }

        iBoundX -= ptTarget.X - pt.X;
        iBoundY -= ptTarget.Y - pt.Y;

    }


    public void CentreOnNode(MapNode node, bool bCentreOnMouse = false)
    {
        CentreOnPoint(New Point3D(CInt(node.Location.X + (node.Width / 2)), CInt(node.Location.Y + (node.Height / 2)), node.Location.Z), bCentreOnMouse);
    }


    public void CentreAxis()
    {

        ' Work out the current grid ref in the centre of the map on the current axis plane
        private Point pt = ConvertScreento3D(new Point(CInt(Me.Width / 2) + iBoundX, CInt(Me.Height / 2) + iBoundY), 0);
        private Point3D ptCentre = new Point3D(CInt(pt.X / iScale), CInt(pt.Y / iScale), 0);

        'Debug.WriteLine(ptCentre.ToString)

        MoveAxis(ptCentre.X, ptCentre.Y);

    }


    private void MoveAxis(int iX, int iY)
    {

        iX = CInt(iX / 2) * 2
        iY = CInt(iY / 2) * 2

        If iX = 0 && iY = 0 Then Exit Sub

        'Dim pt As Point = ConvertScreento3D(New Point(CInt(Me.Width / 2) + iBoundX, CInt(Me.Height / 2) + iBoundY), 0)
        'Dim ptCentre As Point3D = New Point3D(CInt(pt.X / iScale), CInt(pt.Y / iScale), 0)

        'Debug.WriteLine(ptCentre.ToString)

        ' Change the x and y offset by 1 grid's worth
        private Point pt1 = Planes.GetPoint2D(new Point3D(0, 0, 0));
        private Point pt2 = Planes.GetPoint2D(new Point3D(iX, iY, 0));

        private int iMoveX = pt2.X - pt1.X;
        private int iMoveY = pt2.Y - pt1.Y;

        'Debug.WriteLine("X:" & iX & ", Y:" & iY)

        For Each node As MapNode In Page.Nodes
            node.Location.X -= iX;
            node.Location.Y -= iY;
            For Each link As MapLink In node.Links.Values
                For Each p As Point3D In link.OrigMidPoints
                    p.X -= iX;
                    p.Y -= iY;
                Next;
            Next;
        Next;

        iBoundX -= iMoveX;
        iBoundY -= iMoveY;
        RecalculateNodes();
        imgMap.Refresh();

    }


    public void Zoom(bool bIn = true)
    {

        private int iLastScale = iScale;
        if (bIn)
        {
            iScale = Math.Min(CInt(iScale * 1.15), 100)
            If iScale < 10 && iScale = iLastScale Then iScale += 1
        Else
            iScale = Math.Max(CInt(iScale / 1.15), 0)
            If iScale > 1 && iScale = iLastScale Then iScale -= 1
        }
        if (iScale <> iLastScale)
        {

            ' See what % the mouse is at within the node range, then re-set that once we've resized
            private Point ptMouse = imgMap.PointToClient(MousePosition);
            private Rectangle rectRange = MapRange();
            private int pX = CInt(100 * (ptMouse.X - rectRange.X) / Math.Max(rectRange.Width, 1));
            private int pY = CInt(100 * (ptMouse.Y - rectRange.Y) / Math.Max(rectRange.Height, 1));

            ' The location to focus the map back to
            private int iFocusX = ptMouse.X;
            private int iFocusY = ptMouse.Y;

            if (ptMouse.Y < 0 || pX < -50 || pX > 150)
            {
                pX = 50
                iFocusX = CInt(sizeImage.Width / 2)
            }
            if (ptMouse.Y < 0 || pY < -50 || pY > 150)
            {
                pY = 50
                iFocusY = CInt(sizeImage.Height / 2)
            }

            'Debug.WriteLine(String.Format("Mouse position: {0},{1}", ptMouse.X, ptMouse.Y))
            'Debug.WriteLine(String.Format("Currently {0}% and {1}% in nodes", pX, pY))

            RecalculateNodes();

            rectRange = MapRange()

            iBoundX -= iFocusX - (CInt(pX * rectRange.Width / 100) + rectRange.X);
            iBoundY -= iFocusY - (CInt(pY * rectRange.Height / 100) + rectRange.Y);

            RecalculateNodes();
            imgMap.Refresh();
        }

    }

    private void imgMap_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        If e.Delta <> 0 Then Zoom(e.Delta > 1)
    }



    public Point, ByVal pt As PointF) As Boolean PolygonContainsPoint(points()
    {

        ' Check basic rectangle first
        If pt.X < Math.Min(points(0).X, points(3).X) || pt.X > Math.Max(points(1).X, points(2).X) _
            || pt.Y < Math.Min(points(0).Y, points(1).Y) || pt.Y > Math.Max(points(2).Y, points(3).Y) Then Return false;

        ' If inside the rectangle, do a more specific check
        private bool isIn = false;
        private int i = 0, j As Integer = 3;

        Do While i < 4
            If ((((points(i).Y <= pt.Y) && (pt.Y < points(j).Y)) || ((points(j).Y <= pt.Y) && (pt.Y < points(i).Y))) _
                && (pt.X < (points(j).X - points(i).X) * (pt.Y - points(i).Y) / (points(j).Y - points(i).Y) + points(i).X)) Then;
                isIn = Not isIn
            }

            j = i
            i += 1;
        Loop;

        return isIn;

    }



    private void Map_Load(object sender, System.EventArgs e)
    {
        imgMap.BackColor = MAPBACKGROUND;
        RecalculateNodes();
        ToolStrip1.Visible = false;
#if Runner
        btnAddNode.Visible = false;
#endif
    }


    private void btnCentralise_Click(System.Object sender, System.EventArgs e)
    {
        CentreMap();
    }


    private Rectangle MapRange()
    {

        If Page.Nodes.Count = 0 Then Return New Rectangle(0, 0, 0, 0)

        private int iMinX = Integer.MaxValue;
        private int iMinY = Integer.MaxValue;
        private int iMaxX = Integer.MinValue;
        private int iMaxY = Integer.MinValue;
        private bool bFound = false;

        For Each node As MapNode In Page.Nodes
            With node;
                if (node.Seen)
                {
                    If Math.Max(.Points(1).X, .Points(2).X) > iMaxX Then iMaxX = Math.Max(.Points(1).X, .Points(2).X)
                    If Math.Min(.Points(0).X, .Points(3).X) < iMinX Then iMinX = Math.Min(.Points(0).X, .Points(3).X)
                    If Math.Max(.Points(2).Y, .Points(3).Y) > iMaxY Then iMaxY = Math.Max(.Points(2).Y, .Points(3).Y)
                    If Math.Min(.Points(0).Y, .Points(1).Y) < iMinY Then iMinY = Math.Min(.Points(0).Y, .Points(1).Y)
                    bFound = True
                }
            }
        Next;

        If ! bFound Then Return New Rectangle(0, 0, 0, 0)
        return new Rectangle(iMinX, iMinY, iMaxX - iMinX, iMaxY - iMinY);

    }


    public void CentreMap(int iPage = -1)
    {

        If Page == null Then Exit Sub

        '#If www Then
        '        If imgMap.Height = 100 AndAlso imgMap.Width = 200 Then Exit Sub ' Dunno why it's forcing to this size
        '        'RecalculateNodes() - fixes issue going Up/Down at start Jac Jim
        '#End If

        if (Page.Nodes.Count = 0)
        {
            CentreOnPoint(New Point3D(0, 0, 0));
            Exit Sub;
        }
        'Debug.WriteLine(String.Format("Map width:{0}, MinX:{1}, Nodes width:{2}, Need to offset X by {3}", imgMap.Width, iMinX, iMaxX - iMinX, CInt(iMinX + ((iMaxX - iMinX) / 2) - (imgMap.Width / 2))))

        private Rectangle rectRange = MapRange();
        if (rectRange.Width = 0 && rectRange.Height = 0)
        {
            RecalculateNodes(iPage);
            rectRange = MapRange()
        }

        ' Ok, change our offsets
        iBoundX += CInt(rectRange.X + (rectRange.Width / 2) - (sizeImage.Width / 2));
        iBoundY += CInt(rectRange.Y + (rectRange.Height / 2) - (sizeImage.Height / 2));
        'iBoundX += CInt(iMinX + ((iMaxX - iMinX) / 2) - (imgMap.Width / 2))
        'iBoundY += CInt(iMinY + ((iMaxY - iMinY) / 2) - (imgMap.Height / 2))
        if (iPage = -1)
        {
            RecalculateNodes(iPage);
            imgMap.Refresh();
        }

    }


    private void miEditLocation_Click(System.Object sender, System.EventArgs e)
    {
        EditLocation();
    }

    private void miMoveUpDown_Click(object sender, System.EventArgs e)
    {
        MoveUpDown(sender == miMoveUp);
    }


    private void MoveUpDown(bool bUp)
    {
        For Each node As MapNode In SelectedNodes
            node.Location.Z += CInt(IIf(bUp, -1, 1));
            RecalculateNode(node);
        Next;
        imgMap.Refresh();
        'If HotTrackedNode IsNot Nothing Then
        '    HotTrackedNode.Location.Z += CInt(IIf(bUp, -1, 1))
        '    RecalculateNode(HotTrackedNode)
        '    imgMap.Refresh()
        'End If
    }

    private void imgMap_Resize(object sender, System.EventArgs e)
    {
#if Not www
        sizeImage = imgMap.Size
        CentreMap();
#endif
    }

    private void btnZoomIn_Click(System.Object sender, System.EventArgs e)
    {
        Zoom(sender == btnZoomIn);
    }

    private void imgMap_PreviewKeyDown(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
    {
        Debug.WriteLine(e.KeyData);

        'If bRenaming Then
        '    Select Case e.KeyData
        '        Case Keys.Escape
        '            bRenaming = False
        '            Adventure.htblLocations(ActiveNode.Key).ShortDescription = sPreviousName
        '            imgMap.Refresh()
        '        Case Keys.Enter
        '            bRenaming = False
        '            imgMap.Refresh()
        '        Case Keys.Back
        '            Adventure.htblLocations(ActiveNode.Key).ShortDescription = sLeft(Adventure.htblLocations(ActiveNode.Key).ShortDescription, Adventure.htblLocations(ActiveNode.Key).ShortDescription.Length - 1)
        '            imgMap.Refresh()
        '        Case Else
        '            Dim ch As Char = ChrW(e.KeyCode)
        '            If Char.IsLetterOrDigit(ch) OrElse Char.IsPunctuation(ch) Then
        '                Adventure.htblLocations(ActiveNode.Key).ShortDescription &= IIf(e.Shift, UCase(ch), LCase(ch)).ToString
        '            End If
        '            'Select Case ch
        '            '    Case " "c, "a"c To "z"c, "A"c To "Z"c
        '            '        Adventure.htblLocations(ActiveNode.Key).ShortDescription &= IIf(e.Shift, UCase(ch), LCase(ch)).ToString
        '            'End Select
        '            imgMap.Refresh()
        '            'End If
        '    End Select
        '    Exit Sub
        'End If

#if Generator
        switch (e.KeyData)
        {
            case Keys.Control Or Keys.Shift Or Keys.P:
                {
                Print();
            case Keys.T:
                {
                'MoveAxis(1, 1)
                'CentreAxis()
            case Keys.Escape:
                {
                If NewLink != null Then RemoveLink(NewLink)
                ActiveNode = Nothing
            case Keys.Control Or Keys.U:
            case Keys.Control Or Keys.D:
                {
                MoveUpDown(e.KeyData = (Keys.Control | Keys.U));
            case Keys.Delete:
            case Keys.Back:
                {
                private New Generic.List<string> sKeys;
                For Each node As MapNode In SelectedNodes
                    sKeys.Add(node.Key);
                Next;
                DeleteItems(sKeys);
                'Case Keys.Space
                '    If SelectedLink IsNot Nothing Then
                '        SelectedLink.Duplex = Not SelectedLink.Duplex
                '    End If
                'Case Keys.T
                '    If SelectedLink IsNot Nothing Then
                '        If SelectedLink.Style = DashStyle.Solid Then
                '            SelectedLink.Style = DashStyle.Dot
                '        Else
                '            SelectedLink.Style = DashStyle.Solid
                '        End If
                '    End If
            case Keys.Control Or Keys.Shift Or Keys.R:
                {
                ResetMap();
        }
#else
        fRunner.txtInput.Focus();
        'fRunner.txtInput.
        SendKeys.Send(LCase(ChrW(e.KeyCode))) ' not perfect, but will have to do;
#endif

    }



    private void ResetMap()
    {
        if (MessageBox.Show("Are you sure you wish to reset the map?", "Reset Map", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = DialogResult.Yes)
        {
            With Adventure;
                .Map = New clsMap;
                .Map.RecalculateLayout();
                imgMap.Refresh();
            }
        }
    }

    private void RemoveLink(MapLink link)
    {

        If SelectedLink == link Then SelectedLink = null

        private MapNode nodSource = Page.GetNode(link.sSource);
        If nodSource == null Then Exit Sub
        private MapNode nodDest = Page.GetNode(link.sDestination);
        nodSource.Links.Remove(link.eSourceLinkPoint);
        If nodSource != null && nodSource != ActiveNode Then nodSource.Anchors(link.eSourceLinkPoint).Visible = false
        If nodDest != null && nodDest != ActiveNode Then nodDest.Anchors(link.eDestinationLinkPoint).Visible = false
        If NewLink == link Then NewLink = null
        imgMap.Refresh();

        private clsLocation locSource = Adventure.htblLocations(link.sSource);
        locSource.arlDirections(link.eSourceLinkPoint).Restrictions.Clear();
        locSource.arlDirections(link.eSourceLinkPoint).LocationKey = "";

        if (link.sDestination <> "")
        {
            private clsLocation locDest = Adventure.htblLocations(link.sDestination);
            locDest.arlDirections(link.eDestinationLinkPoint).Restrictions.Clear();
            locDest.arlDirections(link.eDestinationLinkPoint).LocationKey = "";
        }

    }


    private void miDeleteLink_Click(System.Object sender, System.EventArgs e)
    {
        If SelectedLink != null Then RemoveLink(SelectedLink)
    }


    'Private Sub DeleteLink(ByVal link As MapLink)
    '    Page.GetNode(link.sSource).Links.Remove(link.eSourceLinkPoint)
    'End Sub


    private void imgMap_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        'bAllowMoveSelected = True
        CentreAxis();
        'imgMap.Refresh()
    }


    private void btnAddNode_Click(System.Object sender, System.EventArgs e)
    {
        AddLocation();
    }


    public void AddLocation()
    {
        AddNode(true);
    }

    private void miAddAnchor_Click(object sender, System.EventArgs e)
    {

        try
        {

            if (SelectedLink IsNot null)
            {
                private Point3D, ptEnd As Point3D ptStart;

                if (HotTrackedAnchor Is null || HotTrackedAnchor.Direction = SelectedLink.eSourceLinkPoint)
                {
                    ' Add anchor between start anchor and first/end anchor
                    ptStart = Page.GetNode(SelectedLink.sSource).Anchors(SelectedLink.eSourceLinkPoint).GetApproxPoint3D
                    if (SelectedLink.OrigMidPoints.Length > 0)
                    {
                        ptEnd = SelectedLink.OrigMidPoints(0)
                    Else
                        ptEnd = Page.GetNode(SelectedLink.sDestination).Anchors(SelectedLink.eDestinationLinkPoint).GetApproxPoint3D
                    }
                Else
                    ' Add anchor between start/second last anchor and last anchor
                    if (SelectedLink.OrigMidPoints.Length > 0)
                    {
                        ptStart = SelectedLink.OrigMidPoints(SelectedLink.OrigMidPoints.Length - 1)
                    Else
                        ptStart = Page.GetNode(SelectedLink.sSource).Anchors(SelectedLink.eSourceLinkPoint).GetApproxPoint3D
                    }
                    ptEnd = Page.GetNode(SelectedLink.sDestination).Anchors(SelectedLink.eDestinationLinkPoint).GetApproxPoint3D
                }

                ' Find the location half way between the source point and either the first point or the destination point
                private New Point3D(CInt((ptStart.X + ptEnd.X) / 2), CInt((ptStart.Y + ptEnd.Y) / 2), CInt((ptStart.Z + ptEnd.Z) / 2)) ptMid;
                ReDim Preserve SelectedLink.OrigMidPoints(SelectedLink.OrigMidPoints.Length);

                ' Create the point mid-way between the other two points
                if (HotTrackedAnchor Is null || HotTrackedAnchor.Direction = SelectedLink.eSourceLinkPoint)
                {
                    for (int i = SelectedLink.OrigMidPoints.Length - 1; i <= 1; i += -1)
                    {
                        SelectedLink.OrigMidPoints(i) = SelectedLink.OrigMidPoints(i - 1);
                    Next;
                    SelectedLink.OrigMidPoints(0) = ptMid;
                Else
                    SelectedLink.OrigMidPoints(SelectedLink.OrigMidPoints.Length - 1) = ptMid;
                }

                private New Anchor a;
                a.Visible = true;
                a.Parent = SelectedLink;
                SelectedLink.Anchors.Add(a);

                RecalculateLinks(Page.GetNode(SelectedLink.sSource));
                imgMap.Refresh();
            }

        }
        catch (Exception ex)
        {
            ErrMsg("Error adding anchor", ex);
        }

    }


    private void miRenameLocation_Click(System.Object sender, System.EventArgs e)
    {
        RenameNode();
    }


    private void RenameNode()
    {
        'bRenaming = True
        'sPreviousName = Adventure.htblLocations(ActiveNode.Key).ShortDescription
        txtRename.Text = Adventure.htblLocations(ActiveNode.Key).ShortDescription.ToString;
        txtRename.Tag = txtRename.Text;
        txtRename.Visible = true;
        txtRename.Focus();
        imgMap.Refresh();
    }



    private void txtRename_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Escape:
                {
                Adventure.htblLocations(ActiveNode.Key).ShortDescription.Item(0).Description = txtRename.Tag.ToString;
                UpdateLocDescription(ActiveNode.Key, txtRename.Tag.ToString);
                ActiveNode.Text = txtRename.Tag.ToString;
                txtRename.Visible = false;
                e.SuppressKeyPress = true;
                e.Handled = true;
            case Keys.Enter:
                {
                txtRename.Visible = false;
                e.SuppressKeyPress = true;
                e.Handled = true;
        }
        imgMap.Refresh();
    }


    private void txtRename_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
    {
        imgMap.Refresh();
    }


    private void txtRename_LostFocus(object sender, System.EventArgs e)
    {
        txtRename.Visible = false;
    }



    private void txtRename_TextChanged(object sender, System.EventArgs e)
    {
        Adventure.htblLocations(ActiveNode.Key).ShortDescription.Item(0).Description = txtRename.Text;
        ActiveNode.Text = txtRename.Text;
        UpdateLocDescription(ActiveNode.Key, txtRename.Text);
        imgMap.Refresh();
    }


    private void miOneWayLink_Click(System.Object sender, System.EventArgs e)
    {

        if (Not SelectedLink.Duplex)
        {
            ' Attempt to make the link duplex.  It may not be possible if the other end already has a link
            if (Adventure.htblLocations(SelectedLink.sDestination).arlDirections(SelectedLink.eDestinationLinkPoint).LocationKey = "")
            {
                Adventure.htblLocations(SelectedLink.sDestination).arlDirections(SelectedLink.eDestinationLinkPoint).LocationKey = SelectedLink.sSource;
                SelectedLink.Duplex = true;
            Else
                ErrMsg("Unable to make the link 2-way as the other end already has an outgoing route.");
            }
        Else
            ' TODO - warn if there are restrictions
            Adventure.htblLocations(SelectedLink.sDestination).arlDirections(SelectedLink.eDestinationLinkPoint).LocationKey = "";
            SelectedLink.Duplex = false;
        }

    }


    private void cmsLink_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        If SelectedLink != null Then miOneWayLink.Checked = ! SelectedLink.Duplex
    }


    private void cmsNode_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {

        miMoveToPage.DropDownItems.Clear();
        For Each iPage As Integer In Map.Pages.Keys
            if (iPage <> Page.iKey)
            {
                private ToolStripItem tsi = miMoveToPage.DropDownItems.Add(Map.Pages(iPage).Label, null, AddressOf MoveToPage);
                tsi.Tag = iPage;
            }
        Next;
        'For Each tab As Infragistics.Win.UltraWinTabControl.UltraTab In tabsMap.Tabs
        '    If SafeInt(tab.Key) <> Page.iKey Then
        '        Dim tsi As ToolStripItem = miMoveToPage.DropDownItems.Add(Map.Pages(SafeInt(tab.Key)).Label, Nothing, AddressOf MoveToPage)
        '        tsi.Tag = SafeInt(tab.Key)
        '    End If
        'Next
        miMoveToPage.Visible = (miMoveToPage.DropDownItems.Count > 0);

    }


    private void MoveToPage(object sender, System.EventArgs e)
    {

        private ToolStripItem tsi = CType(sender, ToolStripItem);
        private int iPage = SafeInt(tsi.Tag);
        private int iPageFrom = ActiveNode.Page;

        For Each n As MapNode In SelectedNodes
            Map.MoveNodeToPage(n, iPage);
        Next;

#if Mono OrElse www
        If ! Map.Pages.ContainsKey(iPageFrom) Then tabsMap.TabPages.Remove(tabsMap.TabPages(iPageFrom.ToString))
        tabsMap.SelectedTab = tabsMap.TabPages(iPage.ToString);
#else
        If ! Map.Pages.ContainsKey(iPageFrom) Then tabsMap.Tabs.Remove(tabsMap.Tabs(iPageFrom.ToString))
        tabsMap.SelectedTab = tabsMap.Tabs(iPage.ToString);
#endif

        imgMap.Refresh();

    }


    private void miRenamePage_Click(System.Object sender, System.EventArgs e)
    {
        private string sTabLabel = InputBox("Enter page label:", "Rename page", Page.Label);
        if (sTabLabel <> "")
        {

#if Mono OrElse www
            private TabPage tab = tabsMap.SelectedTab;
            Map.Pages(SafeInt(tab.Name)).Label = sTabLabel;
#else
            private Infragistics.Win.UltraWinTabControl.UltraTab tab = tabsMap.SelectedTab;
            Map.Pages(SafeInt(tab.Key)).Label = sTabLabel;
#endif
            tab.Text = sTabLabel;

        }
    }

#if Not Mono AndAlso Not www
    public void AddPage()
    {
        With Map;
            private int iNewPage = .GetNewPage(true);
            .Pages.Add(iNewPage, New MapPage(iNewPage));
            tabsMap.Tabs.Clear();
            For Each iPage As Integer In .Pages.Keys
                tabsMap.Tabs.Add(iPage.ToString, .Pages(iPage).Label);
            Next;
            tabsMap.SelectedTab = tabsMap.Tabs(iNewPage);
        }
    }

    private void miAddPage_Click(System.Object sender, System.EventArgs e)
    {
        AddPage();
    }
#endif


#if Not Mono AndAlso Not www
    private void tabsMap_TabMoved(object sender, Infragistics.Win.UltraWinTabControl.TabMovedEventArgs e)
    {
        private New Generic.Dictionary<int, MapPage> NewPages;
        for (int iPosition = 0; iPosition <= Map.Pages.Count - 1; iPosition++)
        {
            For Each tab As Infragistics.Win.UltraWinTabControl.UltraTab In tabsMap.Tabs
                if (tab.VisibleIndex = iPosition)
                {
                    private MapPage page = Map.Pages(SafeInt(tab.Key));
                    NewPages.Add(page.iKey, page);
                    Exit For;
                }
            Next;
        Next;
        Map.Pages = NewPages;
    }
#endif

    private void imgMap_MouseLeave(object sender, System.EventArgs e)
    {
        HotTrackedAnchor = Nothing
        HotTrackedLink = Nothing
        HotTrackedNode = Nothing
    }


    private void miDeleteNode_Click(object sender, System.EventArgs e)
    {
#if Generator
        private New Generic.List<string> sKeys;
        For Each n As MapNode In SelectedNodes
            sKeys.Add(n.Key);
        Next;
        DeleteItems(sKeys);
#endif
    }

    public void New()
    {

        ' This call is required by the designer.
        InitializeComponent();

        ' Add any initialization after the InitializeComponent() call.
        '#If www Then
        '        imgMap.Image = New Bitmap(400, 300) ' Me.ClientSize.Width, Me.ClientSize.Height)
        '#End If
    }


#Region "Trizbort"

#if Generator
    public bool ImportTrizbort(trizbort trizbort)
    {

        If Adventure.htblLocations.Count > 0 Then AddPage()
        Page = Map.Pages(Map.Pages.Count - 1)
        private New Dictionary<int, string> dictIDs;

        With trizbort;
            With .info;
                If Adventure.Title = "Untitled" && .title <> "" Then Adventure.Title = .title
                If Adventure.Author = "Anonymous" && .author <> "" Then Adventure.Author = .author
                if (.title <> "")
                {
                    Page.Label = .title;
                    fGenerator.Map1.tabsMap.SelectedTab.Text = .title;
                }
            }

            For Each oElement As Object In trizbort.map
                if (TypeOf oElement Is trizbortRoom)
                {
                    private trizbortRoom room = CType(oElement, trizbortRoom);

                    private New clsLocation loc;
                    loc.Key = loc.GetNewKey;
                    loc.ShortDescription = New Description(room.name);
                    Adventure.htblLocations.Add(loc, loc.Key);
                    UpdateLocDescription(loc.Key, loc.ShortDescription.ToString);
                    If room.description <> "" Then loc.LongDescription = New Description(room.description)


                    private New MapNode node;
                    dictIDs.Add(room.id, loc.Key);
                    node.Key = loc.Key;
                    node.Page = Page.iKey;
                    node.Location = New Point3D(CInt(room.x / 16), CInt(room.y / 16), 0);
                    node.Width = CInt(room.w / 16);
                    node.Height = CInt(room.h / 16);
                    node.Text = loc.ShortDescriptionSafe;
                    Page.AddNode(node);
                }
            Next;

            ' First pass, map the directions that are exact N, E, SE etc
            ' Second pass, map the ENE etc directions onto the best fit
            '
            for (int iPass = 0; iPass <= 1; iPass++)
            {
                For Each oElement As Object In trizbort.map
                    if (TypeOf oElement Is trizbortLine)
                    {
                        private trizbortLine line = CType(oElement, trizbortLine);
                        private New MapLink link;
                        private bool bOKToAdd = true;
                        private clsLocation locSource = null;
                        private clsLocation locDest = null;

                        if (line.flow = "oneWay")
                        {
                            link.Duplex = false;
                        Else
                            link.Duplex = true;
                        }

                        if (line.dock IsNot null)
                        {
                            if (line.dock.Length > 0)
                            {
                                With line.dock(0);
                                    link.sSource = dictIDs(.id);
                                    locSource = Adventure.htblLocations(link.sSource)
                                    link.eSourceLinkPoint = (DirectionsEnum)(-1);

                                    if (iPass = 0)
                                    {
                                        switch (line.startText)
                                        {
                                            case "up":
                                                {
                                                If locSource.arlDirections(DirectionsEnum.Up).LocationKey = "" Then link.eSourceLinkPoint = DirectionsEnum.Up
                                            case "down":
                                                {
                                                If locSource.arlDirections(DirectionsEnum.Down).LocationKey = "" Then link.eSourceLinkPoint = DirectionsEnum.Down
                                            case "out":
                                                {
                                                if (locSource.arlDirections(DirectionsEnum.In).LocationKey = "")
                                                {
                                                    link.eSourceLinkPoint = DirectionsEnum.In;
                                                    Page.GetNode(link.sSource).bHasIn = true;
                                                }
                                            case "in":
                                                {
                                                if (locSource.arlDirections(DirectionsEnum.Out).LocationKey = "")
                                                {
                                                    link.eSourceLinkPoint = DirectionsEnum.Out;
                                                    Page.GetNode(link.sSource).bHasOut = true;
                                                }
                                        }
                                    Else
                                        switch (line.startText)
                                        {
                                            case "up":
                                            case "down":
                                            case "in":
                                            case "out":
                                                {
                                                bOKToAdd = False
                                        }
                                    }

                                    if (link.eSourceLinkPoint = CType(-1, DirectionsEnum))
                                    {
                                        switch (.port)
                                        {
                                            case "n":
                                                {
                                                If iPass = 0 Then link.eSourceLinkPoint = DirectionsEnum.North Else bOKToAdd = false
                                            case "e":
                                                {
                                                If iPass = 0 Then link.eSourceLinkPoint = DirectionsEnum.East Else bOKToAdd = false
                                            case "s":
                                                {
                                                If iPass = 0 Then link.eSourceLinkPoint = DirectionsEnum.South Else bOKToAdd = false
                                            case "w":
                                                {
                                                If iPass = 0 Then link.eSourceLinkPoint = DirectionsEnum.West Else bOKToAdd = false
                                            case "ne":
                                                {
                                                If iPass = 0 Then link.eSourceLinkPoint = DirectionsEnum.NorthEast Else bOKToAdd = false
                                            case "se":
                                                {
                                                If iPass = 0 Then link.eSourceLinkPoint = DirectionsEnum.SouthEast Else bOKToAdd = false
                                            case "sw":
                                                {
                                                If iPass = 0 Then link.eSourceLinkPoint = DirectionsEnum.SouthWest Else bOKToAdd = false
                                            case "nw":
                                                {
                                                If iPass = 0 Then link.eSourceLinkPoint = DirectionsEnum.NorthWest Else bOKToAdd = false
                                            case "nne":
                                            case "ene":
                                            case "ese":
                                            case "sse":
                                            case "ssw":
                                            case "wsw":
                                            case "wnw":
                                            case "nnw":
                                                {
                                                if (iPass = 1)
                                                {
                                                    link.eSourceLinkPoint = FindSpareLink(locSource, .port, false);
                                                    If link.eSourceLinkPoint = (DirectionsEnum)(-1) Then bOKToAdd = false
                                                Else
                                                    bOKToAdd = False
                                                }
                                        }
                                    }
                                }
                            }

                            if (bOKToAdd && line.dock.Length > 1)
                            {
                                With line.dock(1);
                                    link.sDestination = dictIDs(.id);
                                    locDest = Adventure.htblLocations(link.sDestination)

                                    switch (line.endText)
                                    {
                                        case "up":
                                            {
                                            link.eDestinationLinkPoint = DirectionsEnum.Up;
                                        case "down":
                                            {
                                            link.eDestinationLinkPoint = DirectionsEnum.Down;
                                        case "out":
                                            {
                                            link.eDestinationLinkPoint = DirectionsEnum.In;
                                            Page.GetNode(link.sDestination).bHasIn = true;
                                        case "in":
                                            {
                                            link.eDestinationLinkPoint = DirectionsEnum.Out;
                                            Page.GetNode(link.sDestination).bHasOut = true;
                                        default:
                                            {
                                            switch (.port)
                                            {
                                                case "n":
                                                    {
                                                    link.eDestinationLinkPoint = DirectionsEnum.North;
                                                case "e":
                                                    {
                                                    link.eDestinationLinkPoint = DirectionsEnum.East;
                                                case "s":
                                                    {
                                                    link.eDestinationLinkPoint = DirectionsEnum.South;
                                                case "w":
                                                    {
                                                    link.eDestinationLinkPoint = DirectionsEnum.West;
                                                case "ne":
                                                    {
                                                    link.eDestinationLinkPoint = DirectionsEnum.NorthEast;
                                                case "se":
                                                    {
                                                    link.eDestinationLinkPoint = DirectionsEnum.SouthEast;
                                                case "sw":
                                                    {
                                                    link.eDestinationLinkPoint = DirectionsEnum.SouthWest;
                                                case "nw":
                                                    {
                                                    link.eDestinationLinkPoint = DirectionsEnum.NorthWest;
                                                case "nne":
                                                case "ene":
                                                case "ese":
                                                case "sse":
                                                case "ssw":
                                                case "wsw":
                                                case "wnw":
                                                case "nnw":
                                                    {
                                                    link.eDestinationLinkPoint = FindSpareLink(locDest, .port, ! link.Duplex);
                                            }
                                    }


                                }
                            Else
                                link.sDestination = link.sSource;
                                link.eDestinationLinkPoint = OppositeDirection(link.eSourceLinkPoint);
                            }
                        Else
                            bOKToAdd = False
                        }

                        if (bOKToAdd)
                        {
                            Page.GetNode(link.sSource).Links.Add(link.eSourceLinkPoint, link);
                            if (link.sSource <> "")
                            {
                                With Adventure.htblLocations(link.sSource);
                                    private New clsDirection dir;
                                    dir.LocationKey = link.sDestination;
                                    .arlDirections(link.eSourceLinkPoint) = dir;
                                }
                            }
                            if (link.sDestination <> "" && link.Duplex)
                            {
                                With Adventure.htblLocations(link.sDestination);
                                    private New clsDirection dir;
                                    dir.LocationKey = link.sSource;
                                    .arlDirections(link.eDestinationLinkPoint) = dir;
                                }
                            }
                        }
                    }
                Next;
            Next;

            fGenerator.Map1.tabsMap.SelectedTab = fGenerator.Map1.tabsMap.Tabs(Page.iKey);
            RecalculateNodes();
            imgMap.Refresh();
            UpdateMainTitle();

        }

    }
#endif

    private DirectionsEnum FindSpareLink(clsLocation loc, string sDirection, bool bAllowExist)
    {

        switch (sDirection)
        {
            case "nne":
                {
                For Each d As DirectionsEnum In New DirectionsEnum() {DirectionsEnum.North, DirectionsEnum.NorthEast, DirectionsEnum.NorthWest, DirectionsEnum.East, DirectionsEnum.West, DirectionsEnum.SouthEast, DirectionsEnum.SouthWest, DirectionsEnum.South}
                    If bAllowExist || loc.arlDirections(d).LocationKey = "" Then Return d
                Next;
            case "ene":
                {
                For Each d As DirectionsEnum In New DirectionsEnum() {DirectionsEnum.East, DirectionsEnum.NorthEast, DirectionsEnum.SouthEast, DirectionsEnum.North, DirectionsEnum.South, DirectionsEnum.NorthWest, DirectionsEnum.SouthWest, DirectionsEnum.West}
                    If bAllowExist || loc.arlDirections(d).LocationKey = "" Then Return d
                Next;
            case "ese":
                {
                For Each d As DirectionsEnum In New DirectionsEnum() {DirectionsEnum.East, DirectionsEnum.SouthEast, DirectionsEnum.NorthEast, DirectionsEnum.South, DirectionsEnum.North, DirectionsEnum.SouthWest, DirectionsEnum.NorthWest, DirectionsEnum.West}
                    If bAllowExist || loc.arlDirections(d).LocationKey = "" Then Return d
                Next;
            case "sse":
                {
                For Each d As DirectionsEnum In New DirectionsEnum() {DirectionsEnum.South, DirectionsEnum.SouthEast, DirectionsEnum.SouthWest, DirectionsEnum.East, DirectionsEnum.West, DirectionsEnum.NorthEast, DirectionsEnum.NorthWest, DirectionsEnum.North}
                    If bAllowExist || loc.arlDirections(d).LocationKey = "" Then Return d
                Next;
            case "ssw":
                {
                For Each d As DirectionsEnum In New DirectionsEnum() {DirectionsEnum.South, DirectionsEnum.SouthWest, DirectionsEnum.SouthEast, DirectionsEnum.West, DirectionsEnum.East, DirectionsEnum.NorthWest, DirectionsEnum.NorthEast, DirectionsEnum.North}
                    If bAllowExist || loc.arlDirections(d).LocationKey = "" Then Return d
                Next;
            case "wsw":
                {
                For Each d As DirectionsEnum In New DirectionsEnum() {DirectionsEnum.West, DirectionsEnum.SouthWest, DirectionsEnum.NorthWest, DirectionsEnum.South, DirectionsEnum.North, DirectionsEnum.SouthEast, DirectionsEnum.NorthEast, DirectionsEnum.East}
                    If bAllowExist || loc.arlDirections(d).LocationKey = "" Then Return d
                Next;
            case "wnw":
                {
                For Each d As DirectionsEnum In New DirectionsEnum() {DirectionsEnum.West, DirectionsEnum.NorthWest, DirectionsEnum.SouthWest, DirectionsEnum.North, DirectionsEnum.South, DirectionsEnum.NorthEast, DirectionsEnum.SouthEast, DirectionsEnum.East}
                    If bAllowExist || loc.arlDirections(d).LocationKey = "" Then Return d
                Next;
            case "nnw":
                {
                For Each d As DirectionsEnum In New DirectionsEnum() {DirectionsEnum.North, DirectionsEnum.NorthWest, DirectionsEnum.NorthEast, DirectionsEnum.West, DirectionsEnum.East, DirectionsEnum.SouthWest, DirectionsEnum.SouthEast, DirectionsEnum.South}
                    If bAllowExist || loc.arlDirections(d).LocationKey = "" Then Return d
                Next;
        }

        return CType(-1, DirectionsEnum);

    }


#End Region



#Region "Printing"

    Private WithEvents printDoc As Printing.PrintDocument

    public void Print()
    {

        try
        {
            If printDoc == null Then printDoc = New Printing.PrintDocument

            'Dim dlgPrintPreview As New PrintPreviewDialog
            'With dlgPrintPreview
            '    .Document = printDoc
            '    .ShowDialog()
            'End With
            private New PrintDialog dlgPrint;
            With dlgPrint;
                .Document = printDoc;
                .AllowSomePages = true;
                .AllowCurrentPage = true;
                .AllowSelection = true;
                .PrinterSettings.FromPage = 1;
#if Mono
                .PrinterSettings.ToPage = tabsMap.TabCount;
#elif www
                .PrinterSettings.ToPage = Map.Pages.Count;
#else
                .PrinterSettings.ToPage = tabsMap.Tabs.Count;
#endif

                if (.ShowDialog() = DialogResult.OK)
                {
                    printDoc.Print();
                }
            }

        }
        catch (Exception ex)
        {
            ErrMsg("Error printing out map", ex);
        }

    }


    private void printDoc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        iPrintPage = 0
        oStartPage = Page
        iBoundXStore = iBoundX
        iBoundYStore = iBoundY
        iScaleStore = iScale
    }


    private int iPrintPage = 0;
    private MapPage oStartPage;
    private int iBoundXStore = 0;
    private int iBoundYStore = 0;
    private int iScaleStore = 0;

    private void printDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {

        'Retry:
        '        Dim bPrintThisPage As Boolean = False

        '        Select Case e.PageSettings.PrinterSettings.PrintRange
        '            Case Printing.PrintRange.AllPages
        '                bPrintThisPage = True
        '            Case Printing.PrintRange.CurrentPage
        '                bPrintThisPage = (tabsMap.SelectedTab.Index = iPrintPage)
        '            Case Printing.PrintRange.SomePages
        '                bPrintThisPage = iPrintPage >= e.PageSettings.PrinterSettings.FromPage - 1 AndAlso iPrintPage <= e.PageSettings.PrinterSettings.ToPage - 1
        '            Case Printing.PrintRange.Selection
        '        End Select

        '        If Not bPrintThisPage Then
        '            iPrintPage += 1
        '            If iPrintPage = Map.Pages.Count Then Exit Sub
        '            GoTo Retry
        '        End If


        private int iKey = KeyFromPage(iPrintPage);
        If ! Map.Pages.ContainsKey(iKey) Then Exit Sub

        'Me.sizeImage = New Size(e.MarginBounds.Width, e.MarginBounds.Height)
        Me.sizeImage = New Size(e.PageBounds.Width, e.PageBounds.Height);
        iScale = iScaleStore

        Page = Map.Pages(iKey)

        CentreMap(iKey);
        RecalculateNodes(iKey);
        PaintGraphics(e.Graphics);

        iPrintPage += 1;

        private int iLastPage = Map.Pages.Count;
           Select e.PageSettings.PrinterSettings.PrintRange;
            case Printing.PrintRange.AllPages:
                {
                ' Leave as last page
            case Printing.PrintRange.CurrentPage:
            case Printing.PrintRange.Selection:
                {
#if Mono
                iLastPage = tabsMap.SelectedIndex + 1
#elif Not www
                iLastPage = tabsMap.SelectedTab.Index + 1
#endif
            case Printing.PrintRange.SomePages:
                {
                iLastPage = e.PageSettings.PrinterSettings.ToPage '- 1
        }

        e.HasMorePages = iPrintPage < iLastPage;

    }


    private void printDoc_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        Page = oStartPage
        Me.sizeImage = imgMap.Size;
        iBoundX = iBoundXStore
        iBoundY = iBoundYStore
        iScale = iScaleStore
        RecalculateNodes();
    }


    private void printDoc_QueryPageSettings(object sender, System.Drawing.Printing.QueryPageSettingsEventArgs e)
    {

Retry:;
        private bool bPrintThisPage = false;

        switch (e.PageSettings.PrinterSettings.PrintRange)
        {
            case Printing.PrintRange.AllPages:
                {
                bPrintThisPage = True
            case Printing.PrintRange.CurrentPage:
            case Printing.PrintRange.Selection:
                {
#if Mono
                bPrintThisPage = (tabsMap.SelectedIndex = iPrintPage)
#elif Not www
                bPrintThisPage = (tabsMap.SelectedTab.Index = iPrintPage)
#endif
            case Printing.PrintRange.SomePages:
                {
                bPrintThisPage = iPrintPage >= e.PageSettings.PrinterSettings.FromPage - 1 AndAlso iPrintPage <= e.PageSettings.PrinterSettings.ToPage - 1
        }

        if (Not bPrintThisPage)
        {
            iPrintPage += 1;
            if (iPrintPage = Map.Pages.Count)
            {
                'e.Cancel = True
                Exit Sub;
            }
            GoTo Retry;
        }

        private int iKey = KeyFromPage(iPrintPage);
        Page = Map.Pages(iKey)
        RecalculateNodes(iKey);
        private Rectangle range = MapRange();

        if (range.Width > range.Height)
        {
            e.PageSettings.Landscape = true;
        Else
            e.PageSettings.Landscape = false;
        }
        'e.PageSettings.PrinterSettings.ToPage = tabsMap.Tabs.Count
        if (range.Width > sizeImage.Width)
        {
            iScale = CInt(iScale * (sizeImage.Width / range.Width))
            RecalculateNodes(iKey);
        }

    }


    private int KeyFromPage(int iPage)
    {
        private int iCount = 0;
        For Each Page As Integer In Map.Pages.Keys
            If iPage = iCount Then Return Page
            iCount += 1;
        Next;
        return -1;
    }

#End Region

}
}