using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Simulator
{
    //the background worker
    private static BackgroundWorker Worker = new BackgroundWorker();

    private const int V = 1;
    private static IBl bl = Factory.Get();

    private static event EventHandler<Tuple<Order, int>>? updateSimulation;


    private static volatile bool isSimulationStoped = false;
    //private static Thread? thread;

    //update simulation
    public static void UpdateSimulation(EventHandler<Tuple<Order, int>> action)
    {
        updateSimulation += action;
    }

    // starts the simulation
    public static void StartSimulation()
    {
        Worker.DoWork += (sender, e) => { simulation(); };
        Worker.WorkerSupportsCancellation = true;
        Worker.RunWorkerAsync();

        isSimulationStoped = false;
    }
    //stop the simulation 
    public static void StopSimulation()
    {
        isSimulationStoped = true;
        if (Worker.IsBusy)
        {
            Worker.CancelAsync();
        }
    }
    //it is used to pause the execution of the program
    private static void sleep(int seconds)
    {
        try { Thread.Sleep(seconds * 1000); } catch (ThreadInterruptedException) { }
    }
    //the simulation - initalize the time to handke an order between 3-10 sec and Invokes an order and updates the simulation and updates the updated order in the xml files
    private static void simulation()
    {
        while (!isSimulationStoped && bl.Order.PriorityOrder() != null)
        {
            var order = bl.Order.GetOrder(bl.Order.PriorityOrder() ?? throw new NullReferenceException());
            var timeToHandle = new Random().Next(3, 10);
            var aproximateTime = new Random().Next(timeToHandle, timeToHandle);

            updateSimulation?.Invoke(null, new Tuple<Order, int>(order, aproximateTime));

            sleep(timeToHandle);
            if (isSimulationStoped)
                break;

            if (order.Status == BO.Enums.OrderStatus.Confirmed)
                bl.Order.OrderShippingUpdate(order.Id);
            else if (order.Status == Enums.OrderStatus.Sent)
                bl.Order.OrderDeliveryUpdate(order.Id);
            else
            {
                StopSimulation();
            }
        }
        StopSimulation();

    }
}