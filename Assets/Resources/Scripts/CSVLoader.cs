using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Resources/CSV配下のCSVファイルを読み込むクラス
/// </summary>
public class CSVLoader
{

    /// <summary>
    /// CSVファイルを読み込み配列のList形式で返す
    /// </summary>
    /// <returns>The file.</returns>
    /// <param name="fileName">読み込むファイル名</param>
    /// <param name="delim">区切り文字</param>
    public static List<string[]> ReadFile(string fileName, char delim)
    {

        //Resources/CSV配下のファイルを読み込む
        TextAsset csvFile = Resources.Load("CSV/" + fileName) as TextAsset;

        //StringReaderで一行ずつ読み込んで、区切り文字で分割
        List<string[]> data = new List<string[]>();
        StringReader sr = new StringReader(csvFile.text);
        //文字が読み取れなくなるまで繰り返す
        while (sr.Peek() > -1)
        {
            //1行分取得
            string line = sr.ReadLine();
            //区切り文字で分ける
            data.Add(line.Split(delim));
        }
        return data;
    }
}