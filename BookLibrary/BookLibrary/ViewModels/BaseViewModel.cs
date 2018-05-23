using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace BookLibrary.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set {
                isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }

        //protected bool SetProperty<T>(ref T backingStore, T value,
        //   [CallerMemberName]string propertyName = "",
        //   Action onChanged = null)
        //{
        //    if (EqualityComparer<T>.Default.Equals(backingStore, value))
        //        return false;

        //    backingStore = value;
        //    onChanged?.Invoke();
        //    OnPropertyChanged(propertyName);
        //    return true;
        //}

        //#region INotifyPropertyChanged
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    var changed = PropertyChanged;
        //    if (changed == null)
        //        return;

        //    changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        //#endregion



        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //public string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        //{
        //    var me = propertyLambda.Body as MemberExpression;

        //    if (me == null)
        //    {
        //        throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
        //    }

        //    return me.Member.Name;
        //}
    }
}
