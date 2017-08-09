﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Timers;

namespace Wordki.Helpers {
  public class ListShuffle {
    private static readonly Random Random = new Random();
    public static IList<T> Shuffle<T>(IList<T> list) {
      int n = list.Count;
      while (n > 1) {
        n--;
        int k = Random.Next(n + 1);
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
      }
      return list;
    }
  }

  //============================================================
  public class PackageStore {
    private static Dictionary<int, object> _storeDictionary = new Dictionary<int, object>();
    private static object _object = new object();
    public static bool Put(int pKey, object pObject) {
      lock (_object) {
        if (_storeDictionary.ContainsKey(pKey))
          return false;
        _storeDictionary.Add(pKey, pObject);
      }
      return true;
    }

    public static object Get(int pKey) {
      object lResult;
      lock (_object) {
        if (!_storeDictionary.ContainsKey(pKey))
          return null;
        lResult = _storeDictionary[pKey];
        _storeDictionary.Remove(pKey);
      }
      return lResult;
    }
  }
  //============================================================
  public interface ITimerListener {
    void OnTimerTick(int lTicks);
  }
  [Serializable]
  public class Timer {

    [NonSerialized]
    private List<ITimerListener> _timeListener;
    public List<ITimerListener> TimerListeners {
      get { return _timeListener; }
      set { _timeListener = value; }
    }

    public bool IsRunning { get; private set; }
    public int Ticks { get; private set; }

    [NonSerialized]
    private System.Timers.Timer _timer;

    public Timer() {
      TimerListeners = new List<ITimerListener>();
      _timer = new System.Timers.Timer();
      Ticks = 0;
    }

    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context) {
      TimerListeners = new List<ITimerListener>();
      _timer = new System.Timers.Timer();
    }

    private void OnTimeEvent(object sender, ElapsedEventArgs e) {
      Ticks++;
      UpdateListeners(Ticks);
    }

    public void StartTimer() {
      _timer.Elapsed += OnTimeEvent;
      _timer.Interval = 1000;
      _timer.Enabled = true;
      _timer.Start();
      IsRunning = true;
    }

    public int StopTimer() {
      _timer.Stop();
      IsRunning = false;
      return GetTime();
    }

    public void Pause() {
      _timer.Stop();
      IsRunning = false;
    }

    public void Resume() {
      _timer.Start();
      IsRunning = true;
    }

    public int GetTime() {
      return Ticks;
    }

    private void UpdateListeners(int lTicks) {
      foreach (ITimerListener lTimerListener in TimerListeners) {
        lTimerListener.OnTimerTick(lTicks);
      }
    }

    public static long GetMilis() {
      return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }
  }
  //============================================================
}
