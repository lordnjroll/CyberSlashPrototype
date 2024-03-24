using UnityEngine;
using System.Reflection;


public class GameAssets : MonoBehaviour {

    private static GameAssets _i;

    public static GameAssets i {
        get {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }


    public Sprite s_Potion1;
    public Sprite s_Potion2;
    public Sprite s_PowerUp;
    public Sprite s_SpeedUp;
    public Sprite s_Shield;

}
