﻿using System;
using System.Net.Cache;
using System.Net.Mime;
using System.Windows;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using ScrumBoardPictures.Model;

namespace ScrumBoardPictures.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IContextService _contextService;

        


        private BitmapImage _board = new BitmapImage();

        public BitmapImage Board
        {
            get { return _board; }
            set
            {
                if (_board == value)
                    return;

                _board = value;
                RaisePropertyChanged("Board");
            }
        }

        private string _imageUri;

        public string ImageUri
        {
            get { return _imageUri; }
            set
            {
                if (_imageUri == value)
                    return;

                _imageUri = value;
                RaisePropertyChanged("ImageUri");
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IContextService contextService)
        {
            var overviewUri = new Uri("pack://application:,,,/ScrumBoardPictures;component/BoardOverview.jpg");
            var closeupUri = new Uri("pack://application:,,,/ScrumBoardPictures;component/BoardCloseup.jpg");
            var standupUri = new Uri("pack://application:,,,/ScrumBoardPictures;component/BoardStandup.jpg");

            ImageUri = "pack://application:,,,/ScrumBoardPictures;component/BoardOverview.jpg";

            _contextService = contextService;
            _contextService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.Message);
                    }


                    switch (item)
                    {
                        case BoardState.Overview:
                            ImageUri = "pack://application:,,,/ScrumBoardPictures;component/BoardOverview.jpg";
                            break;

                        case BoardState.Closeup:
                            ImageUri = "pack://application:,,,/ScrumBoardPictures;component/BoardCloseup.jpg";
                            break;

                        case BoardState.Standup:
                            ImageUri = "pack://application:,,,/ScrumBoardPictures;component/BoardStandup.jpg";
                            break;
                    }
                });
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}