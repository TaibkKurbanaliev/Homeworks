using UnityEngine;

public class CollectibleFactory : IEntityFactory<Collectible>
{
    private CollectibleConfig _config;

    public CollectibleFactory(CollectibleConfig config)
    {
        _config = config;
    }

    public Collectible Create(Collectible prefab, Vector3 pos)
    {
        var collectible = Object.Instantiate(prefab, pos, Quaternion.identity);
        collectible.Construct(_config);

        return collectible;
    }
}
