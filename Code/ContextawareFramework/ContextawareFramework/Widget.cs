﻿using System;
using System.Collections.Generic;
using ContextawareFramework.NetworkHelper;

namespace ContextawareFramework
{
    public class Widget
    {
        #region Properties
        private readonly HashSet<IEntity> _trackedEntities = new HashSet<IEntity>();
        private readonly ICommunicationHelper _comHelper;
        private readonly Group _group;


        #endregion
        #region Constructors
        /// <summary>
        /// Constructs a new Widget
        /// </summary>
        /// <param name="comHelper">The communication helper to use. A communication helper is needed for the widget to talk with the context filter</param>
        public Widget(ICommunicationHelper comHelper)
        {
            _comHelper = comHelper;
            _group = new Group(_comHelper);
        }
        #endregion

        /// <summary>
        /// This will start a discovery service that will find any avalible context filters on the local network
        /// </summary>
        public void StartDiscovery()
        {
            Console.WriteLine("Starting discovery (" + _comHelper.Me.Guid + ")");
            _comHelper.DiscoveryServiceEvent += (sender, args) => _group.AddPeer(args.Peer);
            _comHelper.DiscoveryService();
        }

        /// <summary>
        /// This methode should be invoked when ever a tracked entity is updated. This will send the update to the context filter
        /// </summary>
        /// <param name="entity">The entity that was updated</param>
        public void Notify(IEntity entity)
        {
            RegisterEntity(entity);
            _group.SendEntity(entity);
        }

        /// <summary>
        /// Registers the enetity and givs it an unique Id.
        /// </summary>
        /// <param name="entity"></param>
        private void RegisterEntity(IEntity entity)
        {
            if (!_trackedEntities.Contains(entity))
            {
                entity.Id = Guid.NewGuid();
                entity.WidgetId = _comHelper.Me.Guid;
                _trackedEntities.Add(entity);
            }
        }
    }
}