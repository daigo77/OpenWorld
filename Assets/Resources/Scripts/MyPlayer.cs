/// <summary>
/// プレイヤーパラメータを管理
/// </summary>
public class MyPlayer
{
    //シングルトン化
    private static MyPlayer instance;
    public static MyPlayer Instance
    {
        get
        {
            if (instance == null) instance = new MyPlayer();
            return instance;
        }
    }

    //セーブ・ロード用のタグ名
    public const string CURRENT_LEVEL = "CurrentLevel";
    public const string CURRENT_HP = "CurrentHp";
    public const string CURRENT_EXP = "CurrentExp";

    //パラメータ
    int level;
    int hp;
    int atk;
    int spd;
    int nextExp;

    //プロパティ
    public int Level { get { return level; } set { level = value; } }
    public int Hp { get { return hp; } set { hp = value; } }
    public int Atk { get { return atk; } set { atk = value; } }
    public int Spd { get { return spd; } set { spd = value; } }
    public int NextExp { get { return nextExp; } set { nextExp = value; } }

    //初期化（コンストラクタ）
    public MyPlayer()
    {
        level = 1;
        hp = 100;
        atk = 1;
        spd = 1;
        nextExp = 5;
    }
}