/// <summary>
/// �v���C���[�p�����[�^���Ǘ�
/// </summary>
public class MyPlayer
{
    //�V���O���g����
    private static MyPlayer instance;
    public static MyPlayer Instance
    {
        get
        {
            if (instance == null) instance = new MyPlayer();
            return instance;
        }
    }

    //�Z�[�u�E���[�h�p�̃^�O��
    public const string CURRENT_LEVEL = "CurrentLevel";
    public const string CURRENT_HP = "CurrentHp";
    public const string CURRENT_EXP = "CurrentExp";

    //�p�����[�^
    int level;
    int hp;
    int atk;
    int spd;
    int nextExp;

    //�v���p�e�B
    public int Level { get { return level; } set { level = value; } }
    public int Hp { get { return hp; } set { hp = value; } }
    public int Atk { get { return atk; } set { atk = value; } }
    public int Spd { get { return spd; } set { spd = value; } }
    public int NextExp { get { return nextExp; } set { nextExp = value; } }

    //�������i�R���X�g���N�^�j
    public MyPlayer()
    {
        level = 1;
        hp = 100;
        atk = 1;
        spd = 1;
        nextExp = 5;
    }
}