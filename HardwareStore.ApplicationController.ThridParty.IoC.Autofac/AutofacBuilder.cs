using Autofac;
using HardwareStore.ApplicationController.ThridParty.IoC.Abstractions;
using HardwareStore.ApplicationController.ThridParty.IoC.Abstractions.Common;

namespace HardwareStore.ApplicationController.ThridParty.IoC.Autofac;

public sealed class AutofacBuilder() : IIoCContainerBuilder
{
    private readonly ContainerBuilder _containerBuilder = new ContainerBuilder();
    private IContainer _container;

    public IIoCContainerBuilder Register<TService>(Lifetime lifetime = Lifetime.Transient, bool asSelf = false)
        where TService : notnull
    {
        switch (lifetime)
        {
            case Lifetime.Transient:
                _containerBuilder.RegisterType<TService>().Named<TService>($"{nameof(TService)}")
                    .AsSelf();
                break;
            case Lifetime.Singleton:
                _containerBuilder.RegisterType<TService>().Named<TService>($"{nameof(TService)}")
                    .AsSelf()
                    .SingleInstance();
                break;
        }

        return this;
    }

    public IIoCContainerBuilder RegisterWithConstructor<TService>(string nameParameter, object parameter, Lifetime lifetime = Lifetime.Transient, bool asSelf = false)
        where TService : notnull
    {
        switch (lifetime)
        {
            case Lifetime.Transient:
                _containerBuilder.RegisterType<TService>().Named<TService>($"{nameof(TService)}")
                    .AsSelf()
                    .WithParameter(nameParameter, parameter);
                break;
            case Lifetime.Singleton:
                _containerBuilder.RegisterType<TService>().Named<TService>($"{nameof(TService)}")
                    .AsSelf()
                    .SingleInstance()
                    .WithParameter(nameParameter, parameter);
                break;
        }

        return this;
    }

    public IIoCContainerBuilder RegisterInstance<TInstance>(TInstance instance, Lifetime lifetime = Lifetime.Transient, bool asSelf = false)
        where TInstance : class
    {
        switch (lifetime)
        {
            case Lifetime.Transient:
                _containerBuilder.RegisterInstance(instance).Named<TInstance>($"{nameof(TInstance)}")
                    .As<TInstance>();
                break;
            case Lifetime.Singleton:
                _containerBuilder.RegisterInstance(instance).Named<TInstance>($"{nameof(TInstance)}")
                    .As<TInstance>()
                    .SingleInstance();
                break;
        }

        return this;
    }

    public IIoCContainerBuilder RegisterView<TView, TImplementation>(Lifetime lifetime = Lifetime.Transient, bool asSelf = false)
        where TView : notnull
        where TImplementation : TView
    {
        switch (lifetime)
        {
            case Lifetime.Transient:
                _containerBuilder.RegisterType<TImplementation>().Named<TImplementation>($"{nameof(TView)} - {nameof(TImplementation)}")
                    .As<TView>()
                    .AsSelf()
                    .InstancePerDependency();
                break;
            case Lifetime.Singleton:
                _containerBuilder.RegisterType<TImplementation>().Named<TImplementation>($"{nameof(TView)} - {nameof(TImplementation)}")
                    .As<TView>()
                    .AsSelf()
                    .SingleInstance();
                break;
        }

        return this;
    }

    public IIoCContainerBuilder RegisterGeneric<TService>(Lifetime lifetime)
    {
        throw new NotImplementedException();
    }

    public IIoCContainerBuilder RegisterGeneric<TService, TImplementation>(Lifetime lifetime) where TImplementation : TService
    {
        throw new NotImplementedException();
    }

    public IIoCContainerBuilder Register<TService, TImplementation>(Lifetime lifetime = Lifetime.Transient, bool asSelf = false) where TImplementation : TService
    {
        throw new NotImplementedException();
    }

    public TService Resolve<TService>()
        where TService : notnull
    {
        using ILifetimeScope lifetimeScope = _container.BeginLifetimeScope();

        if (!lifetimeScope.IsRegistered<TService>())
            throw new ArgumentException($"{typeof(TService)} не зарегистрирован");

        TService service = lifetimeScope.Resolve<TService>();

        return service;
    }

    public void Build()
        => _container = _containerBuilder.Build();
}