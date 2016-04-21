using FluentAssertions;
using Ninject;
using Ninject.Parameters;
using System.Collections.Generic;
using Ninject.Extensions.Factory;
using Xunit;

namespace NinjectExamples.MultipleSerialPorts
{
    public class SerialPortAddress
    {
        public SerialPortAddress(string address)
        {
            this.Address = address;
        }

        public string Address { get; }
    }

    public interface ISerialPort
    {
        SerialPortAddress Address { get; }
    }

    public class SerialPort : ISerialPort
    {
        public SerialPort(SerialPortAddress address)
        {
            this.Address = address;
        }

        public SerialPortAddress Address { get; }
    }

    public interface IDeviceDriver
    {
        ISerialPort SerialPort { get; }
    }

    public class DeviceDriver : IDeviceDriver
    {
        public DeviceDriver(ISerialPort serialPort)
        {
            SerialPort = serialPort;
        }

        public ISerialPort SerialPort { get; }
    }

    public class Test
    {
        [Fact]
        public void MultiInjection()
        {
            var com1 = new SerialPortAddress("COM1");
            var com2 = new SerialPortAddress("COM2");

            var kernel = new StandardKernel();

            kernel.Bind<ISerialPort>().To<SerialPort>();
            kernel.Bind<IDeviceDriver>().To<DeviceDriver>()
                .WithParameter(new TypeMatchingConstructorArgument(typeof(SerialPortAddress), (ctx, target) => com1, true));
            kernel.Bind<IDeviceDriver>().To<DeviceDriver>()
                .WithParameter(new TypeMatchingConstructorArgument(typeof(SerialPortAddress), (ctx, target) => com2, true));

            var deviceDrivers = kernel.Get<List<IDeviceDriver>>();

            deviceDrivers.Should().HaveCount(2)
                .And.Contain(x => x.SerialPort.Address == com1)
                .And.Contain(x => x.SerialPort.Address == com2);
        }

        [Fact]
        public void NamedBindings()
        {
            const string DeviceDriver1 = "DeviceDriver1";
            const string DeviceDriver2 = "DeviceDriver2";

            var com1 = new SerialPortAddress("COM1");
            var com2 = new SerialPortAddress("COM2");

            var kernel = new StandardKernel();

            kernel.Bind<ISerialPort>().To<SerialPort>();
            kernel.Bind<IDeviceDriver>().To<DeviceDriver>()
                .Named(DeviceDriver1)
                .WithParameter(new TypeMatchingConstructorArgument(typeof(SerialPortAddress), (ctx, target) => com1, true));
            kernel.Bind<IDeviceDriver>().To<DeviceDriver>()
                .Named(DeviceDriver2)
                .WithParameter(new TypeMatchingConstructorArgument(typeof(SerialPortAddress), (ctx, target) => com2, true));

            kernel.Get<IDeviceDriver>(DeviceDriver1).SerialPort.Address.Should().Be(com1);
            kernel.Get<IDeviceDriver>(DeviceDriver2).SerialPort.Address.Should().Be(com2);
        }

        [Fact]
        public void Factory()
        {
            var com1 = new SerialPortAddress("COM1");
            var com2 = new SerialPortAddress("COM2");

            var kernel = new StandardKernel();

            kernel.Bind<ISerialPort>().To<SerialPort>();
            kernel.Bind<IDeviceDriver>().To<DeviceDriver>();
            kernel.Bind<IDeviceDriverFactory>().ToFactory(() => new TypeMatchingArgumentInheritanceInstanceProvider());

            var factory = kernel.Get<IDeviceDriverFactory>();

            factory.Create(com1).SerialPort.Address.Should().Be(com1);
            factory.Create(com2).SerialPort.Address.Should().Be(com2);
        }
    }

    public interface IDeviceDriverFactory
    {
        IDeviceDriver Create(SerialPortAddress address);
    }
}