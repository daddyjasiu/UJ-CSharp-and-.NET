using System;
using System.Collections;
using System.Net.Mail;

namespace Zadanie3
{
    public class Queue1 : ArrayList
    {
        public void Enqueue(Object value)
        {
            base.Insert(0, value);
        }

        public Object Dequeue()
        {
            base.RemoveAt(Count-1);
            return this;
        }
    }

    public class Queue2
    {
        private ArrayList _arrayList = new();

        public void Enqueue(Object value)
        {
            _arrayList.Insert(0, value);
        }

        public Object Dequeue()
        {
            _arrayList?.RemoveAt(_arrayList.Count-1);
            return _arrayList;
        }
    }
    
}