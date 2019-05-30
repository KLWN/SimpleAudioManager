using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Audio;
using UnityEngine;
using UnityEngine.Audio;
#pragma warning disable



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



public class AudioManager : MonoBehaviour
{
    
    public Sound[] soundArray; //Sounds Class greift auf dieses Array zu
    public static AudioManager ManagerInstance; //Prüft ob es nur 1 AM gibt. Public weil shared von allen Instances dieser Class!
    private bool _activated;
    private bool _manipulated;
    
    private float _manipulatedPitch;
    private float _newPitch;
    private float _manipulatedBPM;
    private float _newPitchShifter;
    private float _manipulatedLowPass;
    private float _newLowPass;
    
    private const float _standardPitch = 1f;
    private const float _standardPitchShifter = 1f;
    private const float _standardLowPass = 5000f;



    private void Awake()
    {

        // falls es mehr als 1 AM gibt, destroy das GO auf dem die AM Class liegt
        if (ManagerInstance == null)
        {
            ManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject); //damit wird der AudioManager nicht neugestartet beim Scene-Wechsel (Hauptmenü => Spiel)
        


        //s.audioSource ist die var aus der AM class, s. aus der Sound Class
        foreach (var s in soundArray)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.outputAudioMixerGroup = s.AudioMixerGroup;
            s.audioSource.clip = s.soundFile;                               
            s.audioSource.volume = s.startVolume;
            s.audioSource.loop = s.loop;
            _manipulatedPitch = s.manipulatedPitch;
            _manipulatedBPM = s.manipulatedBPM;
            _manipulatedLowPass = s.manipulatedLowPass;
        }
    }

    
    
    private void Start()
    {
        Play("drum");
    }


    
    private void Update()
    {
        
        // Inputs:
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _activated = !_activated;
        }    
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            _manipulated = !_manipulated;
        }  
        
        
        // Sound-Manipulation Calculations:
        _newPitch = _standardPitch * _manipulatedBPM;
        _newPitchShifter = _standardPitchShifter / _manipulatedBPM;
        
        
        
        // When Sound is Activated:
        if (_activated && !_manipulated)
        {
            foreach (var s in soundArray)
            {
                s.audioSource.volume = s.activatedVolume;
                s.audioSource.outputAudioMixerGroup.audioMixer.SetFloat("PitchMaster", _standardPitch);
                s.audioSource.outputAudioMixerGroup.audioMixer.SetFloat("PitchShifter", _standardPitchShifter);
                s.audioSource.outputAudioMixerGroup.audioMixer.SetFloat("LowPassMaster", _standardLowPass);
            }
        }

        // When Sounds are Manipulated:
        else if (_activated && _manipulated)
        { 
            foreach (var s in soundArray)
            {
                if (s.manipulateVolume)
                {
                    s.audioSource.volume = s.manipulatedVolume;
                }

                if (s.manipulateBPM)
                {
                    s.audioSource.outputAudioMixerGroup.audioMixer.SetFloat("PitchMaster", _newPitch);
                    s.audioSource.outputAudioMixerGroup.audioMixer.SetFloat("PitchShifter", _newPitchShifter);
                }
                
                if (s.manipulatePitch)
                {
                    s.audioSource.outputAudioMixerGroup.audioMixer.SetFloat("PitchMaster", _manipulatedPitch);
                }
                
                if (s.manipulateLowPass)
                {
                    s.audioSource.outputAudioMixerGroup.audioMixer.SetFloat("LowPassMaster", _manipulatedLowPass);
                }
            }
        }
            
        // When Game starts:
        else
        {
            foreach (var s in soundArray)
            {
                s.audioSource.volume = s.startVolume;
                s.audioSource.outputAudioMixerGroup.audioMixer.SetFloat("PitchMaster", _standardPitch);
                s.audioSource.outputAudioMixerGroup.audioMixer.SetFloat("PitchShifter", _standardPitchShifter);
                s.audioSource.outputAudioMixerGroup.audioMixer.SetFloat("LowPassMaster", _standardLowPass);
            }
        }
        
    }

    
    
    // Play Method mit Debugging
    public void Play(string name)
    {
        var s = Array.Find(soundArray,soundMatch => soundMatch.soundName == name);

        if (s == null)
        {
            Debug.LogError("Sound: " + name + " nicht gefunden!");
            return;
        }

        s.audioSource.Play();
    }

    
    
}
