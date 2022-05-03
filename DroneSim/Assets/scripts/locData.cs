using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class locData : MonoBehaviour
{
    public static void putBytes(ref byte[] output, int index, float value)
    {
        //turns a float into its 4 bytes and then puts them into the output array
        //at the given index
        byte[] data = BitConverter.GetBytes(value);
        output[index] = data[0];
        output[index + 1] = data[1];
        output[index + 2] = data[2];
        output[index + 3] = data[3];
    }
    public static void makeFile(Vector3 position)
    {
        //each float is 4 bytes.
        //3 floats in a vector 3(x,y,z) and 3x4 =12!
        byte[] output = new byte[12];
        //get bytes for each part of our lil vector3
        putBytes(ref output, 0, position.x);
        putBytes(ref output, 4, position.y);
        putBytes(ref output, 8, position.z);
        File.WriteAllBytes(Application.dataPath + "/log.txt", output);
    }
    public static void loadFile()
    {
        //converts it all back into pretty print
        if (File.Exists(Application.dataPath + "/log.txt"))
        {
            byte[] input = File.ReadAllBytes(Application.dataPath + "/log.txt");
            int length = input.Length;
            if (length == 12)
            {
                Vector3 ourVector3 = new Vector3();
                ourVector3.x = (float)BitConverter.ToSingle(input, 0);
                ourVector3.y = (float)BitConverter.ToSingle(input, 4);
                ourVector3.z = (float)BitConverter.ToSingle(input, 8);
                print("Position saved in file (" + Application.dataPath + "/log.txt): " + ourVector3.ToString());
            }

        }
    }
}