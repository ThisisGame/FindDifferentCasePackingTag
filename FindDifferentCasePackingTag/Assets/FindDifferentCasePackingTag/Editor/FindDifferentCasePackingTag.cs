/**************************
 * 文件名:FindDifferentCasePackingTag.cs;
 * 文件描述:查找相同字符但是大小写不同的PackingTag;
 * 创建日期:2016/05/30;
 * Author:ThisisGame;
 * Page:https://github.com/ThisisGame/FindDifferentCasePackingTag
 ***************************/

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;


namespace ThisisGame
{
    public class FindDifferentCasePackingTag
    {
        [MenuItem("Assets/FindSameLowerUpperPackingTag")]
        public static void FindErrorPackingTag()
        {
            //PackingTags
            List<string> packingTags = new List<string>();

            //PackingTag -- 文件列表
            Dictionary<string, List<string>> imagePackingTags = new Dictionary<string, List<string>>();

            UnityEngine.Object[] arr = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
            for (int index = 0; index < arr.Length; index++)
            {
                string filePath = AssetDatabase.GetAssetPath(arr[index]);

                //Debug.Log(filePath);

                Sprite sprite = Resources.LoadAssetAtPath<Sprite>(filePath);
                if (sprite == null)
                    continue;

                TextureImporter ti = AssetImporter.GetAtPath(filePath) as TextureImporter;
                if (ti.spritePackingTag.Trim() != string.Empty)
                {
                    if (!imagePackingTags.ContainsKey(ti.spritePackingTag))
                    {
                        imagePackingTags.Add(ti.spritePackingTag, new List<string>());
                    }

                    imagePackingTags[ti.spritePackingTag].Add(filePath);
                }
            }

            //找出存在大小写字母的PackingTag
            foreach (var info in imagePackingTags)
            {
                packingTags.Add(info.Key);
            }

            for (int i = 0; i < packingTags.Count; i++)
            {
                if (packingTags[i] == string.Empty) continue;

                for (int j = 0; j < packingTags.Count; j++)
                {
                    if (packingTags[j] == string.Empty) continue;

                    if (packingTags[j] != packingTags[i] && packingTags[j].ToLower() == packingTags[i].ToLower())
                    {
                        List<string> listA = imagePackingTags[packingTags[i]];
                        List<string> listB = imagePackingTags[packingTags[j]];

                        for (int indexA = 0; indexA < listA.Count; indexA++)
                        {
                            Debug.Log(packingTags[i] + "  : " + listA[indexA]);
                        }

                        for (int indexB = 0; indexB < listB.Count; indexB++)
                        {
                            Debug.LogError(packingTags[j] + "  : " + listB[indexB]);
                        }
                    }
                }

                packingTags[i] = string.Empty;
            }
        }
    }

}
