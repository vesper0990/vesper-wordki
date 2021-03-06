﻿using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Wordki.Helpers {
  public static class InputBindingBehavior {
    public static readonly DependencyProperty InputBindingsSourceProperty =
        DependencyProperty.RegisterAttached
            (
                "InputBindingsSource",
                typeof(IEnumerable),
                typeof(InputBindingBehavior),
                new UIPropertyMetadata(null, InputBindingsSource_Changed)
            );
    public static IEnumerable GetInputBindingsSource(DependencyObject obj) {
      return (IEnumerable)obj.GetValue(InputBindingsSourceProperty);
    }
    public static void SetInputBindingsSource(DependencyObject obj, IEnumerable value) {
      obj.SetValue(InputBindingsSourceProperty, value);
    }

    private static void InputBindingsSource_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
      var uiElement = obj as UIElement;
      if (uiElement == null)
        throw new Exception(String.Format("Object of type '{0}' does not support InputBindings", obj.GetType()));

      uiElement.InputBindings.Clear();
      if (e.NewValue == null)
        return;

      var bindings = (IEnumerable)e.NewValue;
      foreach (var binding in bindings.Cast<InputBinding>())
        uiElement.InputBindings.Add(binding);
    }
  }
}
