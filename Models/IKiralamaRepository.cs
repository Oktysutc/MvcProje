﻿namespace MvcProje.Models
{
    public interface IKiralamaRepository : IRepository<Kiralama>
    {
        void Guncelle(Kiralama Kiralama);
        void Kaydet();

    }
}
