namespace MvcProje.Models
{
    public interface IKitapRepository :IRepository<Kitap>
    {
        void Guncelle(Kitap kitapTuru);
        void Kaydet();

    }
}
