namespace Pet4U.Domain.PetManagement.AgregateRoot
{
    public interface ISoftDeletable
    {
        void Delete();
        void Restore();
    }
}