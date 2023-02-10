using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Game.Events
{
    using SubscribitonStore = Dictionary<string, HashSet<Subscription>>;
    using EventCacheStore = Dictionary<string, Payload>;

    // WIP need more generic version
    public class EventManager
    {
        private SubscribitonStore _subscriptions = new SubscribitonStore();
        private EventCacheStore _caches = new EventCacheStore();

        public Subscription Subscribe(string subscriber, EventName name, Action<Payload> action, bool isTriggerOnSubscribe = false)
        {
            Debug.Log($"Subscriber {subscriber} subscribed event {name.value}");

            Subscription subscription = new Subscription(
                subscriber,
                name,
                action
            );

            if (_subscriptions.TryGetValue(name.value, out HashSet<Subscription> subscriptions))
            {
                if (subscriptions.Add(subscription))
                {
                    return subscription;
                }
                return null;
            }

            Subscription.Comparer comparer = new Subscription.Comparer();
            HashSet<Subscription> newSubscribtions = new HashSet<Subscription>(comparer);
            newSubscribtions.Add(subscription);

            _subscriptions[name.value] = newSubscribtions;

            if (isTriggerOnSubscribe && _caches.TryGetValue(name.value, out Payload e))
            {
                action(e);
            }

            return subscription;
        }

        public bool CancelSubscription(EventName name, Subscription subscription)
        {

            Debug.Log($"Subscriber {subscription.Subscriber} cancelled subscription of event {name.value}");
            if (_subscriptions.TryGetValue(name.value, out HashSet<Subscription> subscriptions))
            {
                subscriptions.Remove(subscription);
                return true;
            }
            return false;
        }

        public void Publish(EventName name, Payload e)
        {
            Debug.Log($"Event {name.value} was published");
            if (_subscriptions.TryGetValue(name.value, out HashSet<Subscription> subscriptions))
            {
                subscriptions.RemoveWhere(s => s.IsCancelled);
                subscriptions.ToList().ForEach(s => s.Receive(e));
            }

            _caches[name.value] = e;
        }

        public void ClearSubscription(EventName name)
        {
            if (_subscriptions.TryGetValue(name.value, out HashSet<Subscription> subscriptions))
            {
                subscriptions.Clear();
            }
        }

        public void ClearSubscriptions()
        {
            _subscriptions.Clear();
            _caches.Clear();
        }
    }

    public class Subscription
    {
        public Subscription(string subscriber, EventName eventName, Action<Payload> action)
        {
            _isCanceled = false;
            _subscriber = subscriber;
            _eventName = eventName;
            _action = action;
        }

        private bool _isCanceled;
        private string _subscriber;
        private EventName _eventName;
        private Action<Payload> _action;


        public string Subscriber { get { return _subscriber; } }
        public EventName EventName { get { return _eventName; } }
        public bool IsCancelled { get { return _isCanceled; } }

        public void Receive(Payload receivedEvent)
        {
            _action(receivedEvent);
        }

        public override bool Equals(object obj)
        {
            return _subscriber?.Equals(obj) ?? false;
        }

        public override int GetHashCode()
        {
            return _subscriber?.GetHashCode() ?? 0;
        }

        public class Comparer : IEqualityComparer<Subscription>
        {
            public bool Equals(Subscription x, Subscription y)
            {
                return x._subscriber?.Equals(y._subscriber) ?? false;
            }

            public int GetHashCode(Subscription obj)
            {
                return obj._subscriber?.GetHashCode() ?? 0;
            }
        }
    }

    public struct Payload
    {
        public object[] args;
    }

    public struct EventName
    {
        public string value;
        public bool isCachable;

        public EventName(string value, bool isCachable)
        {
            this.value = value;
            this.isCachable = isCachable;
        }

        public override string ToString()
        {
            return value;
        }
    }
}