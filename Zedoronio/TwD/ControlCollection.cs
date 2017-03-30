using System.Collections;
using System.Collections.Generic;

namespace Zedoronio.TwD
{
	public class ControlCollection : IList<Control>
	{
		protected Control Parent;
	    protected List<Control> ControlList = new List<Control>();

		public ControlCollection(Control parent)
		{
			Parent = parent;
		}

		public void Add(Control c)
		{
			c.Parent = Parent;
		    ControlList.Add(c);
		}

	    public void Clear()
	    {
	        foreach (var cont  in ControlList)
	        {
	            cont.Parent = null;
	        }
	        ControlList.Clear();
	    }

	    public bool Contains(Control item)
	    {
	        return ControlList.Contains(item);
	    }

	    public void CopyTo(Control[] array, int arrayIndex)
	    {
	        //todo: low
	        throw new System.NotImplementedException();
	    }

	    public bool Remove(Control item)
	    {
	        if (ControlList.Remove(item))
	        {
	            item.Parent = null;
	            return true;
	        }
	        return false;
	    }

	    public int Count => ControlList.Count;

	    public bool IsReadOnly => false;

	    public IEnumerator<Control> GetEnumerator()
	    {
	        return new ControlsEnumerator(this);
	    }

	    IEnumerator IEnumerable.GetEnumerator()
	    {
	        return GetEnumerator();
	    }

	    public int IndexOf(Control item)
	    {
	        return ControlList.IndexOf(item);
	    }

	    public void Insert(int index, Control item)
	    {
	        item.Parent = Parent;
	        ControlList.Insert(index, item);
	    }

	    public void RemoveAt(int index)
	    {
	        ControlList[index].Parent = null;
	        ControlList.RemoveAt(index);
	    }

	    public Control this[int index]
	    {
	        get { return ControlList[index]; }
	        set
	        {
	            value.Parent = Parent;
	            ControlList[index] = value;
	        }
	    }
	}

    public class ControlsEnumerator : IEnumerator<Control>
    {
        private ControlCollection col;
        private int _pos = -1;

        public ControlsEnumerator(ControlCollection col)
        {
            this.col = col;
        }

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            _pos++;
            return _pos < col.Count;
        }

        public void Reset()
        {
            _pos = -1;
        }

        public Control Current => col[_pos];

        object IEnumerator.Current => Current;
    }
}
