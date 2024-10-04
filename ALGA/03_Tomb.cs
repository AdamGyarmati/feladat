using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class TombVerem<T> : Verem<T>
    {
        T[] E;
        int n;
        public bool Ures => n == 0;
       

        public TombVerem(int méret)
        {
            this.E = new T[méret];
            this.n = 0;
        }

        public T Felso()
        {
            if (n > 0)
            {
                return E[n - 1];
            }
            throw new NincsElemKivetel();
        }

        public void Verembe(T ertek)
        {
            if (n <= E.Length - 1)
            {
                E[n] = ertek;
                n++;
            }
            else
            {
                throw new NincsHelyKivetel();
            }
        }

        public T Verembol()
        {
            if (n > 0)
            {
                n--;
                return E[n];
            }
            throw new NincsElemKivetel();
        }
    }
    public class TombSor<T> : Sor<T>
    {
        T[] E;
        int e;
        int u;
        int n;
        public bool Ures => n == 0;

        public TombSor(int meret)
        {
            this.E = new T[meret];
            this.e = 0;
            this.u = 0;
            this.n = 0;
        }

        public T Elso()
        {
            if (n > 0)
            {
                return E[e];
            }
            throw new NincsElemKivetel();
        }

        public void Sorba(T ertek)
        {
            if (n <= E.Length - 1)
            {
                E[u] = ertek;
                //u =(u%E.Length)+1;
                u=(u+1)%E.Length;
                n++;

            }
            else
            {
                throw new NincsHelyKivetel();
            }
        }

        public T Sorbol()
        {
            if (n > 0)
            {
                T elem = E[e];
                e = (e + 1) % E.Length;
                n--;
                return elem;
            }
            throw new NincsElemKivetel();
        }
    }
    public class TombLista<T> : Lista<T>,IEnumerable<T>
    {
        T[] E;
        int n;

        public TombLista()
        {
            E = new T[10];
      
        }

        public int Elemszam => n;

        public T Current => throw new NotImplementedException();


        public void Bejar(Action<T> muvelet)
        {
            for (int i = 0; i < n; i++)
            {
                muvelet(E[i]);
            }
        }

        public void Beszur(int index, T ertek)
        {
            //if (index <= n + 1)
            //{
            //    if (n == E.Length)
            //    {
            //        T[] X = E;
            //        E = new T[E.Length * 2];
            //        for (int i = 0; i < n - 1; i++)
            //        {
            //            E[i] = X[i];
            //        }
            //        X = null;
            //    }

            //    n++;
            //    for (int i = n; i <= index; i++)
            //    {
            //        E[i] = E[i - 1];
            //    }
            //    E[index] = ertek;
            //}
            //else
            //{
            //    throw new HibasIndexKivetel();
            //}
            if (index < 0 || index > n)
            {
                throw new HibasIndexKivetel();
            }
            if (n >= E.Length)
           {
               Novel();
            }
            for (int i = n; i > index; i--)
            {
                E[i] = E[i - 1];
            }
            E[index] = ertek;
            n++;
        }

        public void Dispose()
        {
           
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < n; i++)
            {
                 yield return E[i];
            }
        }

        public void Hozzafuz(T ertek)
        {
            if (n >= E.Length)
            {
                Novel();
            }
            E[n] = ertek;
            n++;
        }
        private void Novel()
        {
            T[] újTömb = new T[E.Length * 2];
            Array.Copy(E, újTömb, n);
            E = újTömb;
        }

        public T Kiolvas(int index)
        {
            if (index < n)
            {
                return E[index];
            }
            else
            {
                throw new HibasIndexKivetel();
            }
        }

        public void Modosit(int index, T ertek)
        {
            if(index <= n)
            {
                E[index] = ertek;
            }
            else
            {
                throw new HibasIndexKivetel();
            }
        }

        public void Torol(T ertek)
        {
            int db = 0;
            for (int i = 0; i <= n-1; i++)
            {
                if (E[i].Equals(ertek))
                {
                    db++;
                }
                else
                {
                    E[i - db] = E[i];
                }
            }
            n = n - db;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
    public class TombListaBejaro<T> : IEnumerator<T>
    {
        T[] E;
        int n;
        int aktualisIndex;

        public T Current => E[n];

        object IEnumerator.Current => throw new NotImplementedException();

        public TombListaBejaro(T[] E,int n)
        {
            this.E = new T[n];
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

        public void Dispose()
        {
            
        }
    }
}
