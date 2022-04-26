using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGridViewscroll
{
    public class TouchGrid
    {
        private readonly DataGridView _View;
        int startDragRowhandle = -1;
        int startDragColumnhandle = -1;
        int topRowIndex = -1;
        int topColumnIndex = -1;
        private bool _IsDragging; 

        public bool IsDragging
        {
            get { return _IsDragging; }
            set { _IsDragging = value; }
        }
        public TouchGrid(DataGridView view)
        {
            _View = view;
            InitViewPropertins();
        }
        private void InitViewPropertins() 
        {
            _View.Cursor = Cursors.Hand;
            _View.MultiSelect = false;
            _View.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _View.MouseDown += _View_MouseDown;
            _View.MouseMove += _View_MouseMove;
            _View.MouseUp += _View_MouseUp;
            _View.Layout += new LayoutEventHandler(_View_Layout);
            _View.DataError += new DataGridViewDataErrorEventHandler(_View_DataError);
        }
        private void _View_DataError(object sender,DataGridViewDataErrorEventArgs e)
        {          
        }

        DataGridView.HitTestInfo myHitTest;
        private int GetRowUnderCursor(Point location)
        {
            myHitTest = _View.HitTest(location.X,location.Y);
            return myHitTest.RowIndex;

        }

        DataGridView.HitTestInfo myHitTest2;
        private int GetColumnUnderCursor(Point location)
        {
            myHitTest2 = _View.HitTest(location.X, location.Y);
            return myHitTest2.ColumnIndex;
        }


        void _View_Layout(object sender,LayoutEventArgs e)
        {
            try {
                IsDragging = false;            
            }
            catch
            {

            }
        }
        private void DoScrollX(int delta)
        {
            if (delta == 0)
                return;
            if (delta < 0 && _View.FirstDisplayedScrollingRowIndex == 0)
                delta = 0;
            _View.FirstDisplayedScrollingRowIndex += delta;

        }
        private void DoScrollY(int delta)
        {
            if (delta == 0)
                return;
            if (delta < 0 && _View.FirstDisplayedScrollingColumnIndex == 0)
                delta = 0;
            _View.FirstDisplayedScrollingColumnIndex += delta;

        }
        private void _View_MouseUp(object sender,MouseEventArgs e)
        {
            try
            {
                IsDragging = false;
            }
            catch
            {}
         
        }
        private void _View_MouseMove(object sender,MouseEventArgs e)
        {
            try
            {
                if (IsDragging)
                {
                    int newRowX = GetRowUnderCursor(e.Location);
                    if (newRowX < 0)
                        return;
                    int deltaX = startDragRowhandle - newRowX;
                    DoScrollX(deltaX);

                    int newRowY = GetColumnUnderCursor(e.Location);
                    if (newRowY < 0)
                        return;
                    int deltaY = startDragColumnhandle - newRowY;
                    DoScrollY(deltaY);
                }
            }
            catch{ }
        }
        private void _View_MouseDown(object sender, MouseEventArgs e)
        {
            try 
            {
                IsDragging = true;
                startDragRowhandle = GetRowUnderCursor(e.Location);
                topRowIndex = _View.FirstDisplayedScrollingRowIndex;

                startDragColumnhandle = GetColumnUnderCursor(e.Location);
                topColumnIndex = _View.FirstDisplayedScrollingColumnIndex;

            }
            catch { }
        }
    }


}
