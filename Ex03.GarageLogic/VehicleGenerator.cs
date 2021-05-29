
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleType = Ex03.GarageLogic.Garage.eVehicleType;

namespace Ex03.GarageLogic
{
    internal class VehicleGenerator
    {

        private  Vehicle m_NewVehicle;

        internal  Vehicle GenerateVehicle(string i_LicensePlateNumber, VehicleType i_VehicleType)
        {
            

            switch (i_VehicleType)
            {

                case VehicleType.FuelCar:
                    {
                        Engine newVehicleEngine = new FuelEngine(FuelEngine.eFuelType.Octan95, (float)FuelEngine.eFuelCapacity.Car);
                        m_NewVehicle = new Car(i_LicensePlateNumber, newVehicleEngine);
                        break;
                    }

                case VehicleType.ElectricCar:
                    {
                        Engine newVehicleEngine = new ElectricEngine(3.2f);
                        m_NewVehicle = new Car(i_LicensePlateNumber, newVehicleEngine);
                        break;
                    }

                case VehicleType.FuelMotorBike:
                    {
                        Engine newVehicleEngine = new FuelEngine(FuelEngine.eFuelType.Octan96, (float)FuelEngine.eFuelCapacity.MotorBike);
                        m_NewVehicle = new MotorBike(i_LicensePlateNumber, newVehicleEngine);
                        break;
                    }

                case VehicleType.ElectricMotorBike:
                    {
                        Engine newVehicleEngine = new ElectricEngine(1.8f);
                        m_NewVehicle = new MotorBike(i_LicensePlateNumber, newVehicleEngine);
                        break;
                    }

                case VehicleType.Truck:
                    {
                        Engine newVehicleEngine = new FuelEngine(FuelEngine.eFuelType.Soler, (float)FuelEngine.eFuelCapacity.Truck);
                        m_NewVehicle = new Truck(i_LicensePlateNumber, newVehicleEngine);
                        break;
                    }
            }

            updateWheels(m_NewVehicle);
            return m_NewVehicle;
        }


        private static void updateWheels(Vehicle io_VehicleToUpdate)
        {
            List<Wheel> wheelsToUpdate = io_VehicleToUpdate.Wheels;

            if(io_VehicleToUpdate is Car)
            {
                for (int i = 0; i < (int)Wheel.eNumberOfWheels.Car; i++)
                {
                    wheelsToUpdate.Add(new Wheel((int)Wheel.eMaxAirPressure.Car));
                }
            }

            else if(io_VehicleToUpdate is MotorBike)
            {
                for (int i = 0; i < (int)Wheel.eNumberOfWheels.MotorBike; i++)
                {
                    wheelsToUpdate.Add(new Wheel((int)Wheel.eMaxAirPressure.MotorBike));
                }
            }

            else
            {
                for (int i = 0; i < (int)Wheel.eNumberOfWheels.Truck; i++)
                {
                    wheelsToUpdate.Add(new Wheel((int)Wheel.eMaxAirPressure.Truck));
                }
            }
        }
    }
}
