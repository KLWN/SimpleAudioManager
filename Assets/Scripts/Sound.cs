using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements;
using UnityEngine;
using UnityEngine.Audio;



 /*@@@@ @@@@@@@  @@@@@@  @@@@@@@   @@@@@@ @@@@@@@@ @@@@@@@@ @@@@@@@ 
!@@       @@!   @@!  @@@ @@!  @@@ !@@     @@!      @@!      @@!  @@@
 !@@!!    @!!   @!@!@!@! @!@!!@!   !@@!!  @!!!:!   @!!!:!   @!@  !@!
    !:!   !!:   !!:  !!! !!: :!!      !:! !!:      !!:      !!:  !!!
::.: :     :     :   : :  :   : : ::.: :  : :: ::: : :: ::: :: :  : 
             _                           
            / | _  __´/_  /_ /_  _ _  _   _ _  _    ._/_
           /_.'/_// //   /_///_|/ / //_' / / //_', / /                                
            _  _/_ _  _  _   _ _      _ _  _  _ /_ ._  _ 
       |/|//_///\_\  /_// / / / //_/ / / //_|/_ / /// //_'
                                 _/                                
                                                                   
.. ..: :.: :.: :.: :.: www.alex.kielwein.com :.: :.: :.: :.: :.. */



[System.Serializable] // Hierdurch wird die Sounds Class serialized
public class Sound
{
    [Header("Basic Loop Settings:")]
    [Tooltip("Dieser Name kann ganz einfach per Play('Name') in der Update Method gecalled werden und wird dann abgespielt.")]
    public string soundName;
    [Tooltip("Hier muss immer die 'Master' Group einzeln angegen werden, nicht der komplette AudioMixer.")]
    public AudioMixerGroup AudioMixerGroup;
    [Tooltip("Welches Sound-File soll abgespielt werden? Es funktionieren WAV und MP3 Dateien.")]
    public AudioClip soundFile;
    [Tooltip("Soll der Sound permanent loopen?")]
    public bool loop = true;
    [HideInInspector] public AudioSource audioSource;
    
    
    [Header("When Game Starts:")]
    [Space (10)]
    [Tooltip("Start-Lautstärke Standard ist: 0")]
    [Range(0f, 1f)] public float startVolume;

    

    [Header("When Sound Is Activated:")]
    [Space (10)]
    [Tooltip("Activated-Lautstärke Standard ist: 1")]
    [Range(0f, 1f)] public float activatedVolume;

    
    
    [Header("When Sounds Are Manipulated:")]
    [Space (10)]
    [Tooltip("Soll die Lautstärke manipuliert werden?")]
    public bool manipulateVolume;
    [Tooltip("Lautstärke Standard ist: 1")]
    [Range(0f, 1f)] public float manipulatedVolume;
    
    [Space(5)]
    
    [Tooltip("Sollen die BPM's manipuliert werden?")]
    public bool manipulateBPM;
    [Tooltip("BPM Standard für 100% ist: 1")]
    [Range(-1f, 3f)] public float manipulatedBPM;
    
    [Space(5)]
    
    [Tooltip("Soll der Pitch manipuliert werden?")]
    public bool manipulatePitch;
    [Tooltip("Pitch Standard ist: 1")]
    [Range(0.1f, 10f)] public float manipulatedPitch;
    
    [Space(5)]
    
    [Tooltip("Soll der LowPass manipuliert werden?")]
    public bool manipulateLowPass;
    [Tooltip("LowPass Standard ist: 5000")]
    [Range(500f, 10000f)] public float manipulatedLowPass;
    
    
    
}
