using System.Linq;
using Classic.Shared.Data;
using LiteDB;

namespace Classic.Shared.Services;

public class RealmlistService
{
    private readonly ILiteCollection<Realm> realms;

    public RealmlistService(ILiteDatabase db)
    {
        this.realms = db.GetCollection<Realm>("realms");
    }

    public Realm[] GetRealms() => this.realms.FindAll().ToArray();
    public void AddRealm(Realm realm) => this.realms.Insert(realm);
    public void RemoveRealm(Realm realm) => this.realms.Delete(realm.Id);
    public void Clear() => this.realms.DeleteAll();
}
