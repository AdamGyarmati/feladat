﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Paradigmak
{
    public class FeltetelesFeladatTarolo<T> : FeladatTarolo<T> where T : IVegrehajthato
    {
        public Func<T, bool>? BejaroFeltetel { get; set; }
        public FeltetelesFeladatTarolo(int meret) : base(meret)
        {

        }
        public void FeltetelesVegrehajtas(Func<T, bool> feltetel)
        {
            for (int i = 0; i < n; i++)
            {
                if (feltetel(tarolo[i]))
                {
                    tarolo[i].Vegrehajtas();
                }
            }
        }
        public override IEnumerator<T> GetEnumerator()
        {
            if (BejaroFeltetel == null)
            {
                return new FeltetelesFeladatTaroloBejaro<T>(tarolo, n, BejaroFeltetel = _ => true);
            }
            else
            {
                return new FeltetelesFeladatTaroloBejaro<T>(tarolo, n, BejaroFeltetel);
            }
        }
    }
    public class FeltetelesFeladatTaroloBejaro<T> : IEnumerator<T>
    {
        T[] tarolo;
        int n;
        int aktualisIndex = -1;
        Func<T, bool> bejaroFeltetel;

        public FeltetelesFeladatTaroloBejaro(T[] tarolo, int n, Func<T, bool> bejaroFeltetel)
        {
            this.tarolo = tarolo;
            this.n = n;
            this.bejaroFeltetel = bejaroFeltetel;
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
            while (aktualisIndex < n - 1)
            {
                aktualisIndex++;
                if (bejaroFeltetel(tarolo[aktualisIndex]))
                {
                    return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            aktualisIndex = -1;
        }
    }
}
