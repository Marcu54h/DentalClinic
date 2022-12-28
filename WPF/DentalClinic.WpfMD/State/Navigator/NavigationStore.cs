﻿using DentalClinic.WpfMD.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DentalClinic.WpfMD.State.Navigator
{
    public class NavigationStore : INavigationStore
    {
        public event Action<IViewType> CurrentViewChanged = default!;

        private IViewType _currentView = default!;
        private IList<IViewType> _queue = new List<IViewType>();
        private int _currentIndex = -1;

        public IViewType CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                AddViewToQueue(CurrentView);
                _currentIndex += 1;
                OnCurrentViewChanged(CurrentView);
            }
        }

        public IViewType Back()
        { 
            if(_currentIndex > 0 )
            {
                _currentIndex -= 1;
                CurrentView = _queue[_currentIndex];
                OnCurrentViewChanged(CurrentView);
            }
            return _currentView;
        }
        

        public IViewType Forward()
        {
            if(_queue.Count > _currentIndex)
            {
                _currentIndex += 1;
                CurrentView = _queue[_currentIndex];
                OnCurrentViewChanged(_currentView);
            }
            return _currentView;
        }

        private void AddViewToQueue(IViewType view)
        {
            if (_queue.Count == 0 )
            {
                _queue.Add(view);
            }
            else
            {
                if (_queue.Last().ViewType != view.ViewType)
                {
                    _queue.Add(view);
                }
            }
        }

        private void OnCurrentViewChanged(IViewType view) =>
            CurrentViewChanged?.Invoke(view);
    }

}
