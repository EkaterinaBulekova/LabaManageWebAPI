using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace LabaManage.BLL.Tests.Fakes
{
    public class FakeDbSet<T> : IDbSet<T> where T : class
    {
        private ObservableCollection<T> data;
        private IQueryable query;

        public FakeDbSet()
        {
            this.data = new ObservableCollection<T>();
            this.query = this.data.AsQueryable();
        }

        public ObservableCollection<T> Local
        {
            get { return this.data; }
        }

        Type IQueryable.ElementType
        {
            get { return this.query.ElementType; }
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return this.query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return this.query.Provider; }
        }

        public virtual T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }

        public T Add(T item)
        {
            this.data.Add(item);
            return item;
        }

        public T Remove(T item)
        {
            this.data.Remove(item);
            return item;
        }

        public T Attach(T item)
        {
            this.data.Add(item);
            return item;
        }

        public T Detach(T item)
        {
            this.data.Remove(item);
            return item;
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.data.GetEnumerator();
        }
    }
}
