﻿using System;
using System.Collections.Generic;
using System.Net;
using ContextawareFramework.NetworkHelper;

namespace ContextawareFramework
{
    public class ContextFilter
    {

        private readonly ICollection<IEntity> _entities = new HashSet<IEntity>(new EntityEquallityCompare());
        private readonly Dictionary<string, ISituation> _situations = new Dictionary<string, ISituation>();

        

        public event EventHandler<SituationChangedEventArgs> SituationStateChanged;
        public void FireSituationStateChanged(ISituation situation, Peer subscriber)
        {
            if(SituationStateChanged != null) SituationStateChanged.Invoke(this, new SituationChangedEventArgs(){Situation = situation, Subscriber = subscriber});
        }
      
        /// <summary>
        /// Add an IEntity instance to the collection beeing checked for situations
        /// </summary>
        /// <param name="entity"></param>
        public void TrackEntity(IEntity entity)
        {
            if(entity == null) throw new ArgumentNullException("Parsed entity can't be null");

            if (_entities.Contains(entity))
            {
                _entities.Remove(entity);
            }
            _entities.Add(entity);
            

            TestSituations();
        }

        /// <summary>
        /// Removes an ISituation instance from the collection of recognized situations
        /// </summary>
        /// <param name="situation">An ISituation instance</param>
        /// <returns></returns>
        public bool RemoveSituation(string situationName)
        {

            if(string.IsNullOrEmpty(situationName)) throw new ArgumentNullException("Parsed situation can't be null");

            return _situations.Remove(situationName);

        }


        /// <summary>
        /// Adds an ISituation instance to the collection of recognized situations
        /// </summary>
        /// <param name="situation">An ISituation instance</param>
        /// <param name="situations">zero or more ISituation instances</param>
        public void AddSituation(ISituation situation, params ISituation[] situations)
        {
            if(situation == null) throw new ArgumentNullException("Parsed situation can't be null");


            //Add the first situation
            _situations.Add(situation.Name, situation);

           
            //Add params if any
            if (situations == null) return;
            foreach (var s in situations)
            {
                _situations.Add(s.Name, s);
            }
        }


        /// <summary>
        /// Subscribes an interesant in a situation given situation name
        /// </summary>
        /// <param name="subscriber">Guid of interesant</param>
        /// <param name="situationIdentifier">situation name</param>
        public void Subscribe(Peer subscriber, string situationName)
        {

            if (subscriber == null) throw new ArgumentNullException("Parsed guid can't be null");
            if (string.IsNullOrEmpty(situationName)) throw new ArgumentNullException("Parsed situationIdentifier can't be null or empty");


            if (_situations.ContainsKey(situationName))
            {
                _situations[situationName].AddSubscriber(subscriber);
            }

        }


        /// <summary>
        /// Tests situation predicates against entities
        /// </summary>
        public void TestSituations()
        {
            foreach (var situation in _situations)
            {
                if (situation.Value.SituationPredicate.Invoke(_entities))
                {
                    foreach (var subscriber in situation.Value.GetSubscribersList())
                    {
                        FireSituationStateChanged(situation.Value, subscriber);
                    }
                }
            }
        }


       
        
    }
}