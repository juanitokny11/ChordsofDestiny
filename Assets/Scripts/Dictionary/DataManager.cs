using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Security.Cryptography;

public static class DataManager
{
    //XML
    public static void SaveToXML<T>(object data, string fileName,String path)
    { // T es para poder definir el tipo de dato que guardamos y object es un tipo de variable generico
        Debug.Log("[DM] SAVING...: " + Time.time);

        //1 definir el path donde se va a guardar 

        //2 comprobar si el path existe 
        // no existe??? crearlo
        CreateDirectory(path);
        //3 crear o sobreescribir el archivo de datos
        //3 escribir "data" dentro del archivo
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (FileStream file = new FileStream(path + "/" + fileName, FileMode.Create))
        {
            serializer.Serialize(file, data);
        }

            Debug.Log("[DM] SAVED: " + Time.time);
    }
    public static object LoadFromXML<T>(String fileName, String path)
    { // T es para que funcione con cualquier tipo de dato
        Debug.Log("[DM] LOADING...: " + Time.time);

        object data;

        //3 cargar el archivo de datos
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (FileStream file = new FileStream(path + "/" + fileName, FileMode.Open))
        {
            data = serializer.Deserialize(file);
        }

        Debug.Log("[DM] LOADED: " + Time.time);

        return data;
    }
    //TEXT
    public static void SaveToText<T>(object data, string fileName, String path)
    {
        //CONVERTIR DATA A TEXTO
       string textData = SerializeToText<T>(data);
        Debug.Log("DM TextData : \n " + textData);
        //ENCRIPTAR EL TEXTO
        textData = Encrypt(textData);
        //GUARDAR EL TEXTO ENCRIPTADO EN UN FICHERO
        CreateDirectory(path);
        StreamWriter writer = new StreamWriter(path + "/" + fileName);
        writer.Write(textData);
        writer.Close();
        //GUARDAR COMO PLAYERPREFS
        //PlayerPrefs.SetString(fileName,textData);
    }
    public static object LoadFromText<T>(string fileName, String path)
    { // T es para que funcione con cualquier tipo de dato
        Debug.Log("[DM] LOADING...: " + Time.time);
        string textData = "";
        //LEER EL TEXTO DE UN FICHERO
        StreamReader reader = new StreamReader(path + "/" + fileName);
        textData=reader.ReadToEnd();
        reader.Close();
        //LEER EL TEXTO DE PLAYERPREFS
        /*if (PlayerPrefs.HasKey(fileName))
        {
            textData = PlayerPrefs.GetString(fileName);
        }*/
        //DESENCRIPTAR TEXTO
        textData = Decrypt(textData);
        //CONVERTIR EL TEXTO A DATOS
        return DeserializeToText<T>(textData);
    }
    public static string LoadTextFromFile(string pathfile)
    {
        TextAsset asset=Resources.Load<TextAsset>(pathfile);
        return asset.text;
    }
    public static string[] ReadLinesFromString(string text)
    {
        StringReader reader = new StringReader(text);
        List<string> lines = new List<string>();
        while (true)
        {
            string oneLine = reader.ReadLine();
            if (oneLine != null) lines.Add(oneLine);
            else break;
        }

        return lines.ToArray();
    }
    //SERIALIZE DATA TO STRING
    public static string SerializeToText<T>(object data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));

        using (StringWriter writer = new StringWriter())
        {
            serializer.Serialize(writer, data);
            return writer.ToString();
        }
    }
    public static object DeserializeToText<T>(string textData)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));

        return serializer.Deserialize(new StringReader(textData));
    }
    //ENCRYPT
    private static string Encrypt(string text)
    {
        try
        {
            string key = "MyKey12345";

            byte[] keyArray;

            byte[] byteText = UTF8Encoding.UTF8.GetBytes(text);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] result = cTransform.TransformFinalBlock(byteText, 0, byteText.Length);
            tdes.Clear();

            text = Convert.ToBase64String(result, 0, result.Length);
        }
        catch (Exception)
        {

        }

        return text;
    }
    private static string Decrypt(string text)
    {
        try
        {
            string key = "MyKey12345";

            byte[] keyArray;

            byte[] byteText = Convert.FromBase64String(text);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] result = cTransform.TransformFinalBlock(byteText, 0, byteText.Length);
            tdes.Clear();

            text = UTF8Encoding.UTF8.GetString(result);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        return text;
    }
    //UTILS
    public static void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
    public static bool FileExists(string filePath)
    {
        if (File.Exists(filePath))
        {
            return true;
        }
        else return false;
    }
    public static void CreateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Debug.Log("[DM] Create new directory: " + Time.time);
            Directory.CreateDirectory(path);
        }
    }
}
