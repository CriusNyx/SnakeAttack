using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EventSystemBaseTest {

    /// <summary>
    /// Verify the integrety of the event system
    /// </summary>
    [Test]
    public void EventSystemBaseTestSimplePasses()
    {
        //Check that the event system is empty
        Assert.True(CEventSystem.Count == 0);

        //create an event, add it to the system, and check the count
        TestEventObject testEvent = new TestEventObject();
        CEventSystem.AddEventHandler(category.category, category.subcategory, testEvent);
        Assert.True(CEventSystem.Count == 1);

        //Check the state of the test class after broadcasting a set true, set false, and foo event
        Assert.True(!testEvent.State);
        CEventSystem.BroadcastEvent(category.category, category.subcategory, new SetTrueEvent());
        Assert.True(testEvent.State);
        CEventSystem.BroadcastEvent(category.category, category.subcategory, new SetFalseEvent());
        Assert.True(!testEvent.State);
        CEventSystem.BroadcastEvent(category.category, category.subcategory, new FooEvent());
        Assert.True(!testEvent.State);

        //remove the event, check the count of the system.
        CEventSystem.RemoveEventHandler(category.category, category.subcategory, testEvent);
        Assert.True(CEventSystem.Count == 0);
    }

    private class TestEventObject : ICEventHandler
    {
        private bool state = false;
        public bool State
        {
            get
            {
                return state;
            }
        }

        public void AcceptEvent(CEvent e)
        {
            if(e is SetFalseEvent)
                state = false;
            else if(e is SetTrueEvent)
                state = true;
        }
    }

    private class SetTrueEvent : CEvent
    {

    }

    private class SetFalseEvent : CEvent
    {

    }

    private class FooEvent : CEvent
    {

    }

    private enum category
    {
        category,
        subcategory
    }
}
