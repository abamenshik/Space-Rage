using UnityEngine;
using System.Collections.Generic;

public static class GameobjectExtension {

    // LayerMask: комбинация слоев, если ваша маска состоит из слоёв 2, 5, 6 и 31, вы получите маску: 10000000000000000000000000110010. В юнити 32 слоя, 1 - слой выбран в маске, 0 - не выбран
    // go.layer: номер слоя (при выборе слоя в объекте подписан номер каждого)
    // 1 << go.layer: к единице мы прибавляем справа столько нулей, какой порядковый номер у слоя объекта, т.е. слой UI имеет номер 5, то будет единица и 5 нулей - 100000, так мы привели номер слоя к маске
    // layer | (1 << go.layer): мы объединяем 2 маски, знак | ИЛИ, т.е. в каждой позиции результата будет 1, если хотя бы в одной позиции операндов есть 1.

    // пример:
    // 110110: допустим это LayerMask, в которой выбраны слои 1, 2, 4, 5
    // ++++++
    // 100000: допустим это GameObject, которой находится на слое 5
    // ======
    // 110110: это мы получим
    // и о боже, результат ничем не отличается от изначальной маски, т.е. он равен маске, а значит GameObject находится в слое, который указан в LayerMask
    // все вычесления происходят в десятичной системы счисления, просто с двоичной проще объяснять

    // и пример если слой объекта не указан в маске
    // 1010: допустим это LayerMask, в которой выбраны слои 1, 3
    // ++++
    // 0100: допустим это GameObject, которой находится на слое 2
    // ====
    // 1110: это мы получим
    // как видим, результат отличается от LayerMask, значит объект находится на слое, не указанном в LayerMask

    public static bool IsInLayer(this GameObject go, LayerMask layer) {
        return layer == (layer | (1 << go.layer));
    }

    public static bool CheckTags(this GameObject gameobject, string[] tags) {
        if (tags != null && tags.Length != 0) {
            foreach (string tag in tags) {
                if (gameobject.CompareTag(tag)) {
                    return true;
                }
            }
            return false;
        }
        return true;
    }
    public static GameObject NearestObject(this GameObject source, GameObject[] gameObjects) {
        GameObject nearest = gameObjects[0];
        float shortestDistance = Vector3.Distance(source.transform.position, gameObjects[0].transform.position);
        foreach (GameObject go in gameObjects) {
            float distance = Vector3.Distance(source.transform.position, go.transform.position);
            if (distance < shortestDistance) {
                nearest = go;
                shortestDistance = distance;
            }
        }
        return nearest;
    }
    public static List<Material> GetAllMaterials(this GameObject go) {
        Renderer[] rends = go.GetComponentsInChildren<Renderer>();
        List<Material> mats = new();
        foreach (Renderer rend in rends) {
            mats.Add(rend.material);
        }
        return mats;
    }
}
