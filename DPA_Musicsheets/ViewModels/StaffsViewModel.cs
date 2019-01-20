using GalaSoft.MvvmLight;
using PSAMControlLibrary;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DPA_Musicsheets.Events;
using DPA_Musicsheets.Music;

namespace DPA_Musicsheets.ViewModels
{
    public class StaffsViewModel : ViewModelBase
    {
        // These staffs will be bound to.
        public ObservableCollection<MusicalSymbol> Staffs { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="musicList">We need the musiclist so it can set our staffs.</param>
        public StaffsViewModel(MusicList musicList)
        {
            Staffs = new ObservableCollection<MusicalSymbol>();
            musicList.musicLoaded += MusicLoader_musicLoaded;
        }

        private void MusicLoader_musicLoaded(object sender, MusicLoadedEventArgs e)
        {
            //throw new NotImplementedException();
            IList<MusicalSymbol> symbols = (List<MusicalSymbol>)e.staffsConverter.convert(e.symbolList);
            Staffs.Clear();
            foreach (var symbol in symbols)
            {
                Staffs.Add(symbol);
            }

        }

        /// <summary>
        /// SetStaffs fills the observablecollection with new symbols. 
        /// We don't want to reset the collection because we don't want other classes to create an observable collection.
        /// </summary>
        /// <param name="symbols">The new symbols to show.</param>
        public void SetStaffs(IList<MusicalSymbol> symbols)
        {
            Staffs.Clear();
            foreach (var symbol in symbols)
            {
                Staffs.Add(symbol);
            }
        }
    }
}
