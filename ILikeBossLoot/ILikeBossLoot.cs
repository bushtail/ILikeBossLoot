using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Servers;

namespace ILikeBossLoot;

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 1)]
public class ILikeBossLoot(DatabaseServer databaseServer) : IOnLoad
{
    public Task OnLoad()
    {
        var itemsDb = databaseServer.GetTables().Templates.Items;
        
        if (itemsDb.TryGetValue(ItemTpl.FACECOVER_TAGILLAS_WELDING_MASK_ZABEY, out var zabey)) MakeLootable(zabey);
        if (itemsDb.TryGetValue(ItemTpl.KNIFE_CHAINED_LABRYS, out var battleaxe)) MakeLootable(battleaxe);
        if (itemsDb.TryGetValue(ItemTpl.KNIFE_SUPERFORS_DB_2020_DEAD_BLOW_HAMMER, out var sledgehammer)) MakeLootable(sledgehammer);
        
        return Task.CompletedTask;
    }

    private static void MakeLootable(TemplateItem tpl)
    {
        var properties = tpl.Properties;
        if (properties == null) return;
        
        properties.UnlootableFromSide = [];
        properties.UnlootableFromSlot = "FirstPrimaryWeapon";
        properties.Unlootable = false;
    }
}