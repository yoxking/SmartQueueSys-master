using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//Download by http://www.codefans.net
namespace MyFormDesinger
{
    public delegate void ClickDelegateHander(string message); //声明一个委托

    public partial class Overlayer : UserControl
    {
        public event ClickDelegateHander ClickEvent;//声明一个事件
        HostFrame _themainhost; //被遮罩的控件容器，通过Overlayer操作该容器（以及其中的子控件） 
        Recter _recter = new Recter(); //被操作控件（容器）周围的方框

        Control _currentCtrl = null; //当前被操作控件

        public Control CurrentCtrl
        {
            get { return _currentCtrl; }
            set { _currentCtrl = value; }
        }

        Point _firstPoint = new Point();
        bool _mouseDown = false;
        DragType _dragType = DragType.None;

        public Overlayer(HostFrame themainhost)
        {
            _themainhost = themainhost;  //默认被操作的是控件容器
            Rectangle r = _themainhost.Bounds;
            r = _themainhost.Parent.RectangleToScreen(r);
            r = this.RectangleToClient(r);
            _recter.Rect = r;
            _recter.IsForm = true;
            InitializeComponent();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams para = base.CreateParams;
                para.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT 透明支持
                return para;
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e) //不画背景
        {
            //base.OnPaintBackground(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (_recter != null) //绘制被操作控件周围的方框
            {
                _recter.Draw(e.Graphics);
            }
            base.OnPaint(e);
        }

        public void setRecter(int x, int y, int width, int height)
        {
            _recter.Rect = new Rectangle(x, y, width, height);
            Invalidate2(false);
        }


        #region 代理所有用户操作
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!_mouseDown) //鼠标形状
            {
                DragType dt = _recter.GetMouseDragType(e.Location);
                switch (dt)
                {
                    case DragType.Top:
                        {
                            Cursor = Cursors.SizeNS;
                            break;
                        }
                    case DragType.RightTop:
                        {
                            Cursor = Cursors.SizeNESW;
                            break;
                        }
                    case DragType.RightBottom:
                        {
                            Cursor = Cursors.SizeNWSE;
                            break;
                        }
                    case DragType.Right:
                        {
                            Cursor = Cursors.SizeWE;
                            break;
                        }
                    case DragType.LeftTop:
                        {
                            Cursor = Cursors.SizeNWSE;
                            break;
                        }
                    case DragType.LeftBottom:
                        {
                            Cursor = Cursors.SizeNESW;
                            break;
                        }
                    case DragType.Left:
                        {
                            Cursor = Cursors.SizeWE;
                            break;
                        }
                    case DragType.Center:
                        {
                            Cursor = Cursors.SizeAll;
                            break;
                        }
                    case DragType.Bottom:
                        {
                            Cursor = Cursors.SizeNS;
                            break;
                        }
                    default:
                        {
                            Cursor = Cursors.Default;
                            break;
                        }
                }
            }
            else
            {
                switch (_dragType) //改变方框位置大小
                {
                    case DragType.Top:
                        {
                            Point delta = new Point(e.Location.X - _firstPoint.X, e.Location.Y - _firstPoint.Y);
                            _recter.Rect = new Rectangle(_recter.Rect.X, _recter.Rect.Y + delta.Y, _recter.Rect.Width, _recter.Rect.Height + delta.Y * (-1));
                            _firstPoint = e.Location;
                            break;
                        }
                    case DragType.RightTop:
                        {
                            Point delta = new Point(e.Location.X - _firstPoint.X, e.Location.Y - _firstPoint.Y);
                            _recter.Rect = new Rectangle(_recter.Rect.X, _recter.Rect.Y + delta.Y, _recter.Rect.Width + delta.X, _recter.Rect.Height + delta.Y * (-1));
                            _firstPoint = e.Location;                       
                            break;
                        }
                    case DragType.RightBottom:
                        {
                            Point delta = new Point(e.Location.X - _firstPoint.X, e.Location.Y - _firstPoint.Y);
                            _recter.Rect = new Rectangle(_recter.Rect.X, _recter.Rect.Y, _recter.Rect.Width + delta.X, _recter.Rect.Height + delta.Y);
                            _firstPoint = e.Location;
                            break;
                        }
                    case DragType.Right:
                        {
                            Point delta = new Point(e.Location.X - _firstPoint.X, e.Location.Y - _firstPoint.Y);
                            _recter.Rect = new Rectangle(_recter.Rect.X, _recter.Rect.Y, _recter.Rect.Width + delta.X, _recter.Rect.Height);
                            _firstPoint = e.Location;
                            break;
                        }
                    case DragType.LeftTop:
                        {
                            Point delta = new Point(e.Location.X - _firstPoint.X, e.Location.Y - _firstPoint.Y);
                            _recter.Rect = new Rectangle(_recter.Rect.X + delta.X, _recter.Rect.Y + delta.Y, _recter.Rect.Width + delta.X * (-1), _recter.Rect.Height + delta.Y * (-1));
                            _firstPoint = e.Location;                     
                            break;
                        }
                    case DragType.LeftBottom:
                        {
                            Point delta = new Point(e.Location.X - _firstPoint.X, e.Location.Y - _firstPoint.Y);
                            _recter.Rect = new Rectangle(_recter.Rect.X + delta.X, _recter.Rect.Y, _recter.Rect.Width + delta.X * (-1), _recter.Rect.Height + delta.Y);
                            _firstPoint = e.Location;                    
                            break;
                        }
                    case DragType.Left:
                        {
                            Point delta = new Point(e.Location.X - _firstPoint.X, e.Location.Y - _firstPoint.Y);
                            _recter.Rect = new Rectangle(_recter.Rect.X + delta.X, _recter.Rect.Y, _recter.Rect.Width + delta.X * (-1), _recter.Rect.Height);
                            _firstPoint = e.Location;
                            break;
                        }
                    case DragType.Center:
                        {
                            Point delta = new Point(e.Location.X - _firstPoint.X, e.Location.Y - _firstPoint.Y);
                            _recter.Rect = new Rectangle(_recter.Rect.X + delta.X, _recter.Rect.Y + delta.Y, _recter.Rect.Width, _recter.Rect.Height);
                            _firstPoint = e.Location;
                            break;
                        }
                    case DragType.Bottom:
                        {
                            Point delta = new Point(e.Location.X - _firstPoint.X, e.Location.Y - _firstPoint.Y);
                            _recter.Rect = new Rectangle(_recter.Rect.X, _recter.Rect.Y, _recter.Rect.Width, _recter.Rect.Height + delta.Y);
                            _firstPoint = e.Location;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
     
            if (_mouseDown)
            {
                Invalidate2(false);
            }
            base.OnMouseMove(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) //左键
            {
                bool flag = false;
                foreach (Control c in _themainhost.Controls) //遍历控件容器 看是否选中其中某一控件
                {
                    Rectangle r = c.Bounds;
                    r = _themainhost.RectangleToScreen(r);
                    r = this.RectangleToClient(r);
                    Rectangle rr = r;
                    rr.Inflate(10, 10);
                    if (rr.Contains(e.Location))
                    {
                        _recter.Rect = r;
                        _currentCtrl = c;
                        //(Parent.Parent.Parent as MyFormDesigner).propertyGrid1.SelectedObject = c; //选中控件
                        _recter.IsForm = false;
                        flag = true;
                        Invalidate2(false);
                        break;
                    }
                }
                if (!flag) //没有控件被选中，判断是否选中控件容器
                {
                    Rectangle r = _themainhost.Bounds;
                    r = Parent.Parent.RectangleToScreen(r);
                    r = this.RectangleToClient(r);
                    if (r.Contains(e.Location))
                    {
                        _recter.Rect = r;
                        _recter.IsForm = true;
                        _currentCtrl = null;
                        //(Parent.Parent.Parent as MyFormDesigner).propertyGrid1.SelectedObject = _themainhost; //选中控件容器
                        Invalidate2(false);
                    }
                }

                DragType dt = _recter.GetMouseDragType(e.Location);  //判断是否可以进行鼠标操作
                if (dt != DragType.None)
                {
                    _mouseDown = true;
                    _firstPoint = e.Location;
                    _dragType = dt;
                }
            }
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) //左键弹起
            {
                _firstPoint = new Point();
                _mouseDown = false;
                _dragType = DragType.None;
                Invalidate2(true);


                if (ClickEvent != null) //判断事件是否被注册
                    ClickEvent("Hello World");
            }
            base.OnMouseUp(e);
        }
        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            drgevent.Effect = DragDropEffects.Copy;
            base.OnDragEnter(drgevent);
        }
        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            try
            {
                string[] strs = (string[])drgevent.Data.GetData(typeof(string[])); //获取拖拽数据
                Control ctrl = ControlHelper.CreateControl(strs[1], strs[0]); //实例化控件

                ctrl.Location = _themainhost.PointToClient(new Point(drgevent.X, drgevent.Y)); //屏幕坐标转换成控件容器坐标
                if (!(ctrl is DateTimePicker))
                {
                    ctrl.Text = strs[1];
                }
                _themainhost.Controls.Add(ctrl);
                ctrl.BringToFront();
                _currentCtrl = ctrl;
                //(Parent.Parent.Parent as MyFormDesigner).propertyGrid1.SelectedObject = ctrl; //选中控件

                //将控件容器坐标转换为Overlayer坐标
                Rectangle r = _themainhost.RectangleToScreen(ctrl.Bounds);
                r = this.RectangleToClient(r);

                _recter.Rect = r;
                _recter.IsForm = false;
                Invalidate2(false);
            }
            catch
            {

            }
            base.OnDragDrop(drgevent);
        }
        #endregion

        public void Invalidate2(bool mouseUp)
        {
            Invalidate();
            if (Parent.Parent != null) //更新父控件
            {
                Rectangle rc = new Rectangle(this.Location, this.Size);
                Parent.Parent.Invalidate(rc, true);	
            }
            if (mouseUp) //鼠标弹起 更新底层控件
            {
                if (_currentCtrl != null) //更新底层控件的位置、大小
                {
                    Rectangle r = _recter.Rect;
                    r = this.RectangleToScreen(r);
                    r = _themainhost.RectangleToClient(r);

                    _currentCtrl.SetBounds(r.Left, r.Top, r.Width, r.Height);
                }
                else //更新控件容器大小
                {
                    Rectangle r = _recter.Rect;
                    r = this.RectangleToScreen(r);
                    r = Parent.Parent.RectangleToClient(r);
                    _themainhost.SetBounds(r.Left, r.Top, r.Width, r.Height);
                }
            }
        }
    }
}
