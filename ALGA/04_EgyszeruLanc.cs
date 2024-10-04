using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class LancElem<T>
    {
        public T tart;
        public LancElem<T> kov;

        public LancElem(T tart, LancElem<T> kov)
        {
            if (tart != null)
            {
                this.tart = tart;
            }
            this.kov = kov;
        }
    }
    public class LancoltVerem<T> : Verem<T>
    {
        private LancElem<T> fej;

        public LancoltVerem()
        {
            if (Ures)
            {
                this.fej = null;
            }
        }

        public bool Ures => fej == null;

        public void Felszabadit(LancElem<T> a)
        {
            while (fej != null)
            {
                LancElem<T> q = fej;
                fej = fej.kov;
                Felszabadit(q);
            }
        }
        public T Felso()
        {
            if (fej != null)
            {
                return fej.tart; // Visszaadjuk a verem tetején lévő elemet
            }
            else
            {
                throw new NincsElemKivetel(); // Kivételt dobunk, ha a verem üres
            }
        }


        public void Verembe(T ertek)
        {
            LancElem<T> uj = new LancElem<T>(ertek, fej);
            fej = uj;
        }

        public T Verembol()
        {
            if (fej != null)
            {
                T ertek = fej.tart;
                LancElem<T> q = fej;
                fej = fej.kov;
                //Felszabadit(q);
                return ertek;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }
    }
    public class LancoltSor<T> : Sor<T>
    {
        LancElem<T> fej;
        LancElem<T> vege;

        public LancoltSor()
        {
            if (Ures)
            {
                this.fej = null;
                this.vege = null;
            }
        }
        public void Felszabadit(LancElem<T> a)
        {
            while (fej != null)
            {
                LancElem<T> q = fej;
                fej = fej.kov;
                Felszabadit(q);
            }

        }
        public bool Ures => fej == null;

        public T Elso()
        {
            if (fej != null)
            {
                return fej.tart;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }

        public void Sorba(T ertek)
        {
            LancElem<T> uj = new LancElem<T>(ertek, null);
            if (vege != null)
            {
                vege.kov = uj;
            }
            else
            {
                fej = uj;
            }
            vege = uj;
        }

        public T Sorbol()
        {
            if (fej != null)
            {
                T ertek = fej.tart;
                fej = fej.kov;

                // Ha a fej null lett, az azt jelenti, hogy az utolsó elemet vettük ki
                if (fej == null)
                {
                    vege = null;
                }

                return ertek;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }

    }
    public class LancoltLista<T> : Lista<T>, IEnumerable<T>
    {
        private LancElem<T> fej;

        public LancoltLista()
        {
            this.fej = null;
        }

        public int Elemszam
        {
            get
            {
                int count = 0;
                LancElem<T> current = fej;
                while (current != null)
                {
                    count++;
                    current = current.kov;
                }
                return count;
            }
        }

        public void Felszabadit(LancElem<T> a)
        {
            while (fej != null)
            {
                LancElem<T> p = fej;
                fej = fej.kov;
                Felszabadit(p);
            }
        }
        public void Bejar(Action<T> muvelet)
        {
            LancElem<T> p = fej;
            while (p != null)
            {
                muvelet(p.tart);
                p = p.kov;
            }
        }

        public void Beszur(int index, T ertek)
        {
            if (index < 0)
            {
                throw new HibasIndexKivetel(); // Az index nem lehet negatív
            }

            if (fej == null && index == 0)
            {
                // Ha a lista üres, és az index 0, akkor beszúrjuk az első elemet
                LancElem<T> uj = new LancElem<T>(ertek, null);
                fej = uj;
            }
            else if (index == 0)
            {
                // Ha az index 0, akkor az elejére szúrjuk be az elemet
                LancElem<T> uj = new LancElem<T>(ertek, fej);
                fej = uj;
            }
            else
            {
                // Egyéb esetek: a megfelelő helyre szúrjuk be
                LancElem<T> p = fej;
                int i = 0;
                while (p != null && i < index - 1) // Megkeressük a megfelelő helyet
                {
                    p = p.kov;
                    i++;
                }

                if (p != null)
                {
                    // Ha megtaláltuk a helyet, beszúrjuk az elemet
                    LancElem<T> uj = new LancElem<T>(ertek, p.kov);
                    p.kov = uj;
                }
                else
                {
                    throw new HibasIndexKivetel(); // Ha az index túl nagy, kivételt dobunk
                }
            }
        }

        public void Hozzafuz(T ertek)
        {
            LancElem<T> uj = new LancElem<T>(ertek, null);
            if (fej == null)
            {
                fej = uj;
            }
            else
            {
                LancElem<T> p = fej;
                while (p.kov != null)
                {
                    p = p.kov;
                }
                p.kov = uj;
            }
        }

        public T Kiolvas(int index)
        {
            if (index < 0)
            {
                throw new HibasIndexKivetel(); // Az index nem lehet negatív
            }

            LancElem<T> p = fej;
            int i = 0; // Nulláról induló indexelés
            while (p != null && i < index) // && helyes az index eléréséhez
            {
                p = p.kov;
                i++;
            }

            if (p != null)
            {
                return p.tart; // Ha megtaláltuk az elemet, visszaadjuk
            }
            else
            {
                throw new HibasIndexKivetel(); // Ha nincs ilyen index, kivételt dobunk
            }
        }


        public void Modosit(int index, T ertek)
        {
            if (index < 0)
            {
                throw new HibasIndexKivetel(); // Az index nem lehet negatív
            }

            LancElem<T> p = fej;
            int i = 0; // Nulláról induló indexelés
            while (p != null && i < index) // Helyes feltétel && operátorral
            {
                p = p.kov;
                i++;
            }

            if (p != null)
            {
                p.tart = ertek; // Ha megtaláltuk az elemet, módosítjuk
            }
            else
            {
                throw new HibasIndexKivetel(); // Ha az index túl nagy, kivételt dobunk
            }
        }


        public void Torol(T ertek)
        {
            LancElem<T> p = fej;
            LancElem<T> e = null;

            while (p != null)
            {
                // Ha megtaláltuk az eltávolítandó elemet
                if (p.tart.Equals(ertek))
                {
                    if (e == null)
                    {
                        // Ha az első elemet töröljük
                        fej = p.kov;
                    }
                    else
                    {
                        // Ha nem az első elemet töröljük
                        e.kov = p.kov;
                    }

                    // Nem kell hívni a Felszabadit metódust itt, mivel a .NET Garbage Collector automatikusan felszabadítja a memóriát
                }
                else
                {
                    // Ha nem az eltávolítandó elemet találtuk meg, akkor e-t előrelépjük
                    e = p;
                }

                // p előrelép a következő elemre
                p = p.kov;
            }
        }


        public IEnumerator<T> GetEnumerator()
        {
            LancElem<T> current = fej;
            while (current != null)
            {
                yield return current.tart;
                current = current.kov;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
    public class LancoltListaBejaro<T> : IEnumerator<T>
    {
        private LancElem<T> fej;
        private LancElem<T> jelen;

        public LancoltListaBejaro(LancElem<T> fej)
        {
            this.fej = fej;
            this.jelen = null;
        }

        public T Current
        {
            get
            {
                if (jelen == null)
                {
                    throw new HibasIndexKivetel();
                }
                else
                {
                    return jelen.tart;
                }
            }
        }

        object IEnumerator.Current => jelen;

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (jelen == null)
            {
                jelen = fej;
            }
            else
            {
                jelen = jelen.kov;
            }

            return jelen != null;
        }

        public void Reset()
        {
            jelen = null;
        }
    }
}
