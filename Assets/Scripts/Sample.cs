using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using Cysharp.Threading.Tasks;

public class Sample : MonoBehaviour
{
    private ReactiveProperty<int> m_rp = new ReactiveProperty<int>();
    //public IObservable<int> RP => m_rp;

    private async void Start()
    {
        Debug.Log("�l���ς��̂�ҋ@");
        UniTask.Void(async () =>
        {
            Debug.Log("3�b�҂�");
            await Observable.Timer(TimeSpan.FromSeconds(3));
            Debug.Log("3�b�҂���");
            m_rp.Value = 0;
        });
        await m_rp;
        Debug.Log("�l���ς����");
    }
}
