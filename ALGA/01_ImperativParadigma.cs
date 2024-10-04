using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Paradigmak
{
    public interface IVegrehajthato
    {
        public void Vegrehajtas();
    }
    public class FeladatTarolo<T> : IEnumerable<T> where T : IVegrehajthato
    {
        protected T[] tarolo;
        protected int n;

        public FeladatTarolo(int meret)
        {
            this.tarolo = new T[meret];
            this.n = 0;
        }
        public void Felvesz(T elem)
        {
            if (n < tarolo.Length)
            {
                tarolo[n] = elem;
                n++;
            }
            else
            {
                throw new TaroloMegteltKivetel();
            }
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return new FeladatTaroloBejaro<T>(tarolo, n);
        }

        public virtual void MindentVegrehajt()
        {
            for (int i = 0; i < tarolo.Length; i++)
            {
                if (tarolo[i] != null)
                {
                    tarolo[i].Vegrehajtas();
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
       
    }
    public interface IFuggo
    {
        public bool FuggosegTeljesul { get; }
    }
    public class FuggoFeladatTarolo<T> : FeladatTarolo<T> where T : IVegrehajthato, IFuggo
    {
        public FuggoFeladatTarolo(int meret) : base(meret)
        {
        }
        public override void MindentVegrehajt()
        {
            for (int i = 0; i < tarolo.Length; i++)
            {
                if (tarolo[i] != null && tarolo[i].FuggosegTeljesul)
                {
                    tarolo[i].Vegrehajtas();
                }
            }
        }
    }
    public class TaroloMegteltKivetel : Exception
    {
    }
    public class FeladatTaroloBejaro<T> : IEnumerator<T>
    {
        T[] tarolo;
        int n;
        int aktualisIndex = -1;

        public FeladatTaroloBejaro(T[] tarolo, int n)
        {
            this.tarolo = tarolo;
            this.n = n;
        }

        public T Current
        {
            get
            {
                return tarolo[aktualisIndex];
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (aktualisIndex < n - 1)
            {
                aktualisIndex++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            aktualisIndex = -1;
        }
    }
}
