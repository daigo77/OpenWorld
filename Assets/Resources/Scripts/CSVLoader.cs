using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Resources/CSV�z����CSV�t�@�C����ǂݍ��ރN���X
/// </summary>
public class CSVLoader
{

    /// <summary>
    /// CSV�t�@�C����ǂݍ��ݔz���List�`���ŕԂ�
    /// </summary>
    /// <returns>The file.</returns>
    /// <param name="fileName">�ǂݍ��ރt�@�C����</param>
    /// <param name="delim">��؂蕶��</param>
    public static List<string[]> ReadFile(string fileName, char delim)
    {

        //Resources/CSV�z���̃t�@�C����ǂݍ���
        TextAsset csvFile = Resources.Load("CSV/" + fileName) as TextAsset;

        //StringReader�ň�s���ǂݍ���ŁA��؂蕶���ŕ���
        List<string[]> data = new List<string[]>();
        StringReader sr = new StringReader(csvFile.text);
        //�������ǂݎ��Ȃ��Ȃ�܂ŌJ��Ԃ�
        while (sr.Peek() > -1)
        {
            //1�s���擾
            string line = sr.ReadLine();
            //��؂蕶���ŕ�����
            data.Add(line.Split(delim));
        }
        return data;
    }
}