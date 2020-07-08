namespace Library.CrossCuttingConcerns.Authorization.Enums
{
    public enum PermissionScope
    {
        None = 0,
        User = 1,
        BusinessUnit = 2,
        BusinessUnitAndChildren = 4,
        Organization = 8
    }
}