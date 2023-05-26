using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class CFirebase : MonoBehaviour
{
    DatabaseReference m_Reference;

    int a = 0;
    void Start()
    {
        m_Reference = FirebaseDatabase.DefaultInstance.RootReference;


        //m_Reference = FirebaseDatabase.DefaultInstance.RootReference;

    }

    public void WriteUserData(string num, string word)
    {
        //이거추가해야함
        m_Reference = FirebaseDatabase.DefaultInstance.RootReference;
        m_Reference.Child("turn").Child(num).SetValueAsync(word);
    }

}