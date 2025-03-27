using ConsoleOrderExecutor.ConsoleFunction;
using ConsoleOrderExecutor.ConsoleFunction.Utils;
using ConsoleOrderExecutor.Orders.DTOs;
using ConsoleOrderExecutor.Orders.Services;
using ConsoleOrderExecutor.Products.DTOs;
using ConsoleOrderExecutor.Products.Services;
using FakeItEasy;

namespace ConsoleOrderExecutor.Tests.ConsoleFunctionTests
{
    public class ConsoleFunctionTests
    {
        private readonly IConsoleUtils _consoleUtils;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public ConsoleFunctionTests()
        {
            _consoleUtils = A.Fake<IConsoleUtils>();
            _orderService = A.Fake<IOrderService>();
            _productService = A.Fake<IProductService>();
        }
        [Fact]
        public async Task ConsoleFunctions_CreateNewOrder_OrderCreated()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? companyOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Is this order for company or physical person? Type 1 if company or 0 if person.", A<Predicate<string?>>.Ignored, out companyOut)).Returns(false);
            string? addressOut = "address";
            A.CallTo(() => _consoleUtils.GetParameter("Please write delivery address (max 250 characters).", A<Predicate<string?>>.Ignored, out addressOut)).Returns(false);
            var paymentList = new List<GetPaymentOption>() {
                new() {
                    Id = 2,
                    Name = ""
                }
            };
            var paymentInfoInString = paymentList.Select(x => x.Id + " - " + x.Name);
            A.CallTo(() => _orderService.GetPaymentOptions()).Returns(paymentList);
            string? paymentOut = "2";
            A.CallTo(() => _consoleUtils.GetParameter("Please choose the payment option, by writing its number.\n" + String.Join("\n", paymentInfoInString), A<Predicate<string?>>.Ignored, out paymentOut)).Returns(false);
            string? eanOut = "2342343";
            A.CallTo(() => _consoleUtils.GetParameter("Please write ean of product you want to add.", A<Predicate<string?>>.Ignored, out eanOut)).Returns(false);
            A.CallTo(() => _productService.ProductExist(A<string>.Ignored)).Returns(true);
            string? priceOut = "20.5";
            A.CallTo(() => _consoleUtils.GetParameter("Please write product price.", A<Predicate<string?>>.Ignored, out priceOut)).Returns(false);
            string? nextStepOut = "done";
            A.CallTo(() => _consoleUtils.GetParameter("If you done write done, otherwise next.", A<Predicate<string?>>.Ignored, out nextStepOut)).Returns(false);
            A.CallTo(() => _orderService.CreateOrder(A<CreateOrder>.Ignored)).Returns(true);

            //Act
            await consoleFunctions.CreateNewOrder();

            //Assert
            Assert.DoesNotContain("Error:", strWriter.ToString());
            Assert.Contains("Order created.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_CreateNewOrder_OrderCreationFailed()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? companyOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Is this order for company or physical person? Type 1 if company or 0 if person.", A<Predicate<string?>>.Ignored, out companyOut)).Returns(false);
            string? addressOut = "address";
            A.CallTo(() => _consoleUtils.GetParameter("Please write delivery address (max 250 characters).", A<Predicate<string?>>.Ignored, out addressOut)).Returns(false);
            var paymentList = new List<GetPaymentOption>() {
                new() {
                    Id = 2,
                    Name = ""
                }
            };
            var paymentInfoInString = paymentList.Select(x => x.Id + " - " + x.Name);
            A.CallTo(() => _orderService.GetPaymentOptions()).Returns(paymentList);
            string? paymentOut = "2";
            A.CallTo(() => _consoleUtils.GetParameter("Please choose the payment option, by writing its number.\n" + String.Join("\n", paymentInfoInString), A<Predicate<string?>>.Ignored, out paymentOut)).Returns(false);
            string? eanOut = "2342343";
            A.CallTo(() => _consoleUtils.GetParameter("Please write ean of product you want to add.", A<Predicate<string?>>.Ignored, out eanOut)).Returns(false);
            A.CallTo(() => _productService.ProductExist(A<string>.Ignored)).Returns(true);
            string? priceOut = "20.5";
            A.CallTo(() => _consoleUtils.GetParameter("Please write product price.", A<Predicate<string?>>.Ignored, out priceOut)).Returns(false);
            string? nextStepOut = "done";
            A.CallTo(() => _consoleUtils.GetParameter("If you done write done, otherwise next.", A<Predicate<string?>>.Ignored, out nextStepOut)).Returns(false);
            A.CallTo(() => _orderService.CreateOrder(A<CreateOrder>.Ignored)).Returns(false);

            //Act
            await consoleFunctions.CreateNewOrder();

            //Assert
            Assert.DoesNotContain("Error:", strWriter.ToString());
            Assert.Contains("Failed to create order.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_CreateNewOrder_ProductNameIsNull()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? companyOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Is this order for company or physical person? Type 1 if company or 0 if person.", A<Predicate<string?>>.Ignored, out companyOut)).Returns(false);
            string? addressOut = "address";
            A.CallTo(() => _consoleUtils.GetParameter("Please write delivery address (max 250 characters).", A<Predicate<string?>>.Ignored, out addressOut)).Returns(false);
            var paymentList = new List<GetPaymentOption>() {
                new() {
                    Id = 2,
                    Name = ""
                }
            };
            var paymentInfoInString = paymentList.Select(x => x.Id + " - " + x.Name);
            A.CallTo(() => _orderService.GetPaymentOptions()).Returns(paymentList);
            string? paymentOut = "2";
            A.CallTo(() => _consoleUtils.GetParameter("Please choose the payment option, by writing its number.\n" + String.Join("\n", paymentInfoInString), A<Predicate<string?>>.Ignored, out paymentOut)).Returns(false);
            string? eanOut = "2342343";
            A.CallTo(() => _consoleUtils.GetParameter("Please write ean of product you want to add.", A<Predicate<string?>>.Ignored, out eanOut)).Returns(false);
            A.CallTo(() => _productService.ProductExist(A<string>.Ignored)).Returns(false);
            string? nameOut = null;
            A.CallTo(() => _consoleUtils.GetParameter("Please write product name.", A<Predicate<string?>>.Ignored, out nameOut)).Returns(false);
            string? priceOut = "20.5";
            A.CallTo(() => _consoleUtils.GetParameter("Please write product price.", A<Predicate<string?>>.Ignored, out priceOut)).Returns(false);

            //Act
            await consoleFunctions.CreateNewOrder();

            //Assert
            A.CallTo(() => _orderService.CreateOrder(A<CreateOrder>.Ignored)).MustNotHaveHappened();
            Assert.Contains("Error: Product name is null.", strWriter.ToString());
            Assert.DoesNotContain("Product added.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_CreateNewOrder_ProductCreationFailed()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? companyOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Is this order for company or physical person? Type 1 if company or 0 if person.", A<Predicate<string?>>.Ignored, out companyOut)).Returns(false);
            string? addressOut = "address";
            A.CallTo(() => _consoleUtils.GetParameter("Please write delivery address (max 250 characters).", A<Predicate<string?>>.Ignored, out addressOut)).Returns(false);
            var paymentList = new List<GetPaymentOption>() {
                new() {
                    Id = 2,
                    Name = ""
                }
            };
            var paymentInfoInString = paymentList.Select(x => x.Id + " - " + x.Name);
            A.CallTo(() => _orderService.GetPaymentOptions()).Returns(paymentList);
            string? paymentOut = "2";
            A.CallTo(() => _consoleUtils.GetParameter("Please choose the payment option, by writing its number.\n" + String.Join("\n", paymentInfoInString), A<Predicate<string?>>.Ignored, out paymentOut)).Returns(false);
            string? eanOut = "2342343";
            A.CallTo(() => _consoleUtils.GetParameter("Please write ean of product you want to add.", A<Predicate<string?>>.Ignored, out eanOut)).Returns(false);
            A.CallTo(() => _productService.ProductExist(A<string>.Ignored)).Returns(false);
            string? nameOut = "name";
            A.CallTo(() => _consoleUtils.GetParameter("Please write product name.", A<Predicate<string?>>.Ignored, out nameOut)).Returns(false);
            string? priceOut = "20.5";
            A.CallTo(() => _consoleUtils.GetParameter("Please write product price.", A<Predicate<string?>>.Ignored, out priceOut)).Returns(false);
            A.CallTo(() => _productService.CreateProduct(A<CreateProduct>.Ignored)).Returns(false);

            //Act
            await consoleFunctions.CreateNewOrder();

            //Assert
            A.CallTo(() => _orderService.CreateOrder(A<CreateOrder>.Ignored)).MustNotHaveHappened();
            Assert.Contains("Error: Could not add product.", strWriter.ToString());
            Assert.DoesNotContain("Product added.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_ShowProducts_ProductCountLessThan5_ProductShown()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            var products = new List<GetProduct> {
                new()
                {
                    Id = 1,
                    Ean = "123",
                    Name = "name"
                }
            };
            A.CallTo(() => _productService.GetProducts()).Returns(products);
            var product = products[0];
            var consoleResult = $"id: {product.Id} ean: {product.Ean} name: {product.Name}";

            //Act
            await consoleFunctions.ShowProducts();

            //Assert
            Assert.Contains(consoleResult, strWriter.ToString());
            Assert.DoesNotContain("Error: Found null product.", strWriter.ToString());
            Assert.DoesNotContain("If you want to exit write exit. If you want to see more click enter or write anything.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_ShowProducts_ProductCountGreaterThan5_ProductShown()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            var strReader = new StringReader(Environment.NewLine);
            Console.SetIn(strReader);
            var products = A.CollectionOfFake<GetProduct>(6);
            A.CallTo(() => _productService.GetProducts()).Returns(products);
            var consoleResult = "If you want to exit write exit. If you want to see more click enter or write anything.";

            //Act
            await consoleFunctions.ShowProducts();

            //Assert
            Assert.Contains(consoleResult, strWriter.ToString());
            Assert.DoesNotContain("Error: Found null product.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_ShowProducts_ProductCountGreaterThan5_UserExited_ProductShown()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            var strReader = new StringReader("exit");
            Console.SetIn(strReader);
            var products = A.CollectionOfFake<GetProduct>(6);
            A.CallTo(() => _productService.GetProducts()).Returns(products);
            var consoleResult = "If you want to exit write exit. If you want to see more click enter or write anything.";

            //Act
            await consoleFunctions.ShowProducts();

            //Assert
            Assert.Contains(consoleResult, strWriter.ToString());
            Assert.DoesNotContain("Error: Found null product.", strWriter.ToString());
            Assert.DoesNotContain("End of products.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_ShowOrders_OrderCountLessThan5_OrdersShown()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            var orders = new List<GetOrder> {
                new()
                {
                    Id = 1,
                    OrderValue = 20.5m,
                    Products =
                    [
                        new GetOrderProduct()
                        {
                            Id = 1,
                            Name = "name",
                            Ean = "123",
                            Price = 20.5m
                        }
                    ],
                    OrderType = "Company",
                    DeliveryAddress = "address",
                    StatusName = "Nowe",
                    PaymentOption = "Karta"
                }
            };
            A.CallTo(() => _orderService.GetOrders()).Returns(orders);
            var order = orders[0];
            var consoleOrderResult =
                $"Order id: {order.Id}\r\nValue: {order.OrderValue} PLN; Status: {order.StatusName}; Payment option: {order.PaymentOption};\r\n"
                + $"Type: {order.OrderType}; Address: {order.DeliveryAddress};\r\nProducts:";
            var prod = order.Products.Select(x => $"\tId: {x.Id} Ean: {x.Ean} Name: {x.Name} Price: {x.Price}");
            var consoleProdResult = String.Join("\n", prod);

            //Act
            await consoleFunctions.ShowOrders();

            //Assert
            Assert.Contains(consoleOrderResult, strWriter.ToString());
            Assert.Contains(consoleProdResult, strWriter.ToString());
            Assert.DoesNotContain("Error: Found null order.", strWriter.ToString());
            Assert.DoesNotContain("If you want to exit write exit. If you want to see more click enter or write anything.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_ShowOrders_OrderCountGreaterThan5_OrdersShown()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            var strReader = new StringReader(Environment.NewLine);
            Console.SetIn(strReader);
            var orders = A.CollectionOfFake<GetOrder>(6);
            A.CallTo(() => _orderService.GetOrders()).Returns(orders);
            var consoleResult = "If you want to exit write exit. If you want to see more click enter or write anything.";

            //Act
            await consoleFunctions.ShowOrders();

            //Assert
            Assert.Contains(consoleResult, strWriter.ToString());
            Assert.DoesNotContain("Error: Found null order.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_ShowOrders_OrderCountGreaterThan5_UserExited_OrdersShown()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            var strReader = new StringReader("exit");
            Console.SetIn(strReader);
            var orders = A.CollectionOfFake<GetOrder>(6);
            A.CallTo(() => _orderService.GetOrders()).Returns(orders);
            var consoleResult = "If you want to exit write exit. If you want to see more click enter or write anything.";

            //Act
            await consoleFunctions.ShowOrders();

            //Assert
            Assert.Contains(consoleResult, strWriter.ToString());
            Assert.DoesNotContain("Error: Found null order.", strWriter.ToString());
            Assert.DoesNotContain("End of orders.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_SendOrder_SuccessfullyCreatedThread_WriteSuccessMessage()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? orderIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the order that you want to send.", A<Predicate<string?>>.Ignored, out orderIdOut)).Returns(false);
            A.CallTo(() => _orderService.OrderExist(A<int>.Ignored)).Returns(true);
            A.CallTo(() => _orderService.GetStatusId("W magazynie")).Returns(1);
            A.CallTo(() => _orderService.GetOrderStatusId(A<int>.Ignored)).Returns(1);
            A.CallTo(() => _orderService.GetStatusId("W wysyłce")).Returns(2);

            //Act
            await consoleFunctions.SendOrder();

            //Assert
            Assert.DoesNotContain("Error:", strWriter.ToString());
            Assert.Contains($"The order has been sent for shipment. Please check later if status of order {orderIdOut} has been changed.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_SendOrder_OrderDoNotExist_WriteError()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? orderIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the order that you want to send.", A<Predicate<string?>>.Ignored, out orderIdOut)).Returns(false);
            A.CallTo(() => _orderService.OrderExist(A<int>.Ignored)).Returns(false);

            //Act
            await consoleFunctions.SendOrder();

            //Assert
            Assert.Contains($"Error: The order with id {orderIdOut} do not exist in database.", strWriter.ToString());
            Assert.DoesNotContain($"The order has been sent for shipment. Please check later if status of order {orderIdOut} has been changed.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_SendOrder_StatusWarehouseDoNotExist_WriteError()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? orderIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the order that you want to send.", A<Predicate<string?>>.Ignored, out orderIdOut)).Returns(false);
            A.CallTo(() => _orderService.OrderExist(A<int>.Ignored)).Returns(true);
            A.CallTo(() => _orderService.GetStatusId("W magazynie")).Returns(0);

            //Act
            await consoleFunctions.SendOrder();

            //Assert
            Assert.Contains("Error: Could not find status with the name W magazynie", strWriter.ToString());
            Assert.DoesNotContain($"The order has been sent for shipment. Please check later if status of order {orderIdOut} has been changed.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_SendOrder_OrderHaveDifferentStatusThenWarehouse_WriteError()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? orderIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the order that you want to send.", A<Predicate<string?>>.Ignored, out orderIdOut)).Returns(false);
            A.CallTo(() => _orderService.OrderExist(A<int>.Ignored)).Returns(true);
            A.CallTo(() => _orderService.GetStatusId("W magazynie")).Returns(1);
            A.CallTo(() => _orderService.GetOrderStatusId(A<int>.Ignored)).Returns(3);

            //Act
            await consoleFunctions.SendOrder();

            //Assert
            Assert.Contains("Error: Cannot change given order status, because the order status is not W magazynie.", strWriter.ToString());
            Assert.DoesNotContain($"The order has been sent for shipment. Please check later if status of order {orderIdOut} has been changed.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_SendOrder_StatusSendDONotExist_WriteError()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? orderIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the order that you want to send.", A<Predicate<string?>>.Ignored, out orderIdOut)).Returns(false);
            A.CallTo(() => _orderService.OrderExist(A<int>.Ignored)).Returns(true);
            A.CallTo(() => _orderService.GetStatusId("W magazynie")).Returns(1);
            A.CallTo(() => _orderService.GetOrderStatusId(A<int>.Ignored)).Returns(1);
            A.CallTo(() => _orderService.GetStatusId("W wysyłce")).Returns(0);

            //Act
            await consoleFunctions.SendOrder();

            //Assert
            Assert.Contains("Error: Could not find status with the name W wysyłce", strWriter.ToString());
            Assert.DoesNotContain($"The order has been sent for shipment. Please check later if status of order {orderIdOut} has been changed.", strWriter.ToString());
        }

        [Fact]
        public async Task ConsoleFunctions_PassOrderToWarehouse_ChangedStatusToWMagazynie_WriteSuccessMessage()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? orderIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the order that you want to move to warehouse.", A<Predicate<string?>>.Ignored, out orderIdOut)).Returns(false);
            A.CallTo(() => _orderService.OrderExist(A<int>.Ignored)).Returns(true);
            decimal orderValue = 20.5m;
            A.CallTo(() => _orderService.GetOrderValue(A<int>.Ignored)).Returns(orderValue);
            A.CallTo(() => _orderService.GetPaymentOptionId("Gotówka przy odbiorze")).Returns(2);
            A.CallTo(() => _orderService.GetOrderPaymentOptionId(A<int>.Ignored)).Returns(2);
            A.CallTo(() => _orderService.GetStatusId("Nowe")).Returns(1);
            A.CallTo(() => _orderService.GetOrderStatusId(A<int>.Ignored)).Returns(1);
            A.CallTo(() => _orderService.GetStatusId("W magazynie")).Returns(2);
            A.CallTo(() => _orderService.ModifyOrder(A<ModifyOrder>.Ignored)).Returns(true);

            //Act
            await consoleFunctions.PassOrderToWarehouse();

            //Assert
            Assert.Contains($"Order status with id {orderIdOut} has been changed to W magazynie.", strWriter.ToString());
            Assert.DoesNotContain($"Error:", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_PassOrderToWarehouse_ChangedStatusToReturned_WriteSuccessMessage()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? orderIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the order that you want to move to warehouse.", A<Predicate<string?>>.Ignored, out orderIdOut)).Returns(false);
            A.CallTo(() => _orderService.OrderExist(A<int>.Ignored)).Returns(true);
            decimal orderValue = 2600m;
            A.CallTo(() => _orderService.GetOrderValue(A<int>.Ignored)).Returns(orderValue);
            A.CallTo(() => _orderService.GetPaymentOptionId("Gotówka przy odbiorze")).Returns(2);
            A.CallTo(() => _orderService.GetOrderPaymentOptionId(A<int>.Ignored)).Returns(2);
            A.CallTo(() => _orderService.GetStatusId("Nowe")).Returns(1);
            A.CallTo(() => _orderService.GetOrderStatusId(A<int>.Ignored)).Returns(1);
            A.CallTo(() => _orderService.GetStatusId("Zwrócono do klienta")).Returns(2);
            A.CallTo(() => _orderService.ModifyOrder(A<ModifyOrder>.Ignored)).Returns(true);

            //Act
            await consoleFunctions.PassOrderToWarehouse();

            //Assert
            A.CallTo(() => _orderService.GetStatusId("Zwrócono do klienta")).MustHaveHappened();
            Assert.Contains("Waring: The order have payment option set as Gotówka przy odbiorze and it value exceed 2500. The status will be changed to Zwrócono do klienta.", strWriter.ToString());
            Assert.Contains($"Order status with id {orderIdOut} has been changed to Zwrócono do klienta.", strWriter.ToString());
            Assert.DoesNotContain($"Error:", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_PassOrderToWarehouse_OrderDoNotExist_WriteErrorMessage()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? orderIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the order that you want to move to warehouse.", A<Predicate<string?>>.Ignored, out orderIdOut)).Returns(false);
            A.CallTo(() => _orderService.OrderExist(A<int>.Ignored)).Returns(false);

            //Act
            await consoleFunctions.PassOrderToWarehouse();

            //Assert
            Assert.Contains($"Error: The order with id {orderIdOut} do not exist in database.", strWriter.ToString());
            Assert.DoesNotContain($"Order status with id {orderIdOut} has been changed", strWriter.ToString());
            Assert.DoesNotContain($"Error: Could not change order status with id {orderIdOut}.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_PassOrderToWarehouse_PaymentOptionCashDoNotExist_WriteErrorMessage()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? orderIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the order that you want to move to warehouse.", A<Predicate<string?>>.Ignored, out orderIdOut)).Returns(false);
            A.CallTo(() => _orderService.OrderExist(A<int>.Ignored)).Returns(true);
            decimal orderValue = 20.5m;
            A.CallTo(() => _orderService.GetOrderValue(A<int>.Ignored)).Returns(orderValue);
            A.CallTo(() => _orderService.GetPaymentOptionId("Gotówka przy odbiorze")).Returns(0);

            //Act
            await consoleFunctions.PassOrderToWarehouse();

            //Assert
            Assert.Contains("Error: Could not find payment status with the name Gotówka przy odbiorze", strWriter.ToString());
            Assert.DoesNotContain($"Order status with id {orderIdOut} has been changed", strWriter.ToString());
            Assert.DoesNotContain($"Error: Could not change order status with id {orderIdOut}.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_PassOrderToWarehouse_StatusNewNotFound_WriteErrorMessage()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? orderIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the order that you want to move to warehouse.", A<Predicate<string?>>.Ignored, out orderIdOut)).Returns(false);
            A.CallTo(() => _orderService.OrderExist(A<int>.Ignored)).Returns(true);
            decimal orderValue = 20.5m;
            A.CallTo(() => _orderService.GetOrderValue(A<int>.Ignored)).Returns(orderValue);
            A.CallTo(() => _orderService.GetPaymentOptionId("Gotówka przy odbiorze")).Returns(2);
            A.CallTo(() => _orderService.GetOrderPaymentOptionId(A<int>.Ignored)).Returns(2);
            A.CallTo(() => _orderService.GetStatusId("Nowe")).Returns(0);

            //Act
            await consoleFunctions.PassOrderToWarehouse();

            //Assert
            Assert.Contains("Error: Could not find status with the name Nowe", strWriter.ToString());
            Assert.DoesNotContain($"Order status with id {orderIdOut} has been changed", strWriter.ToString());
            Assert.DoesNotContain($"Error: Could not change order status with id {orderIdOut}.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_PassOrderToWarehouse_OrderHasDifferentStatusThenNew_WriteErrorMessage()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? orderIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the order that you want to move to warehouse.", A<Predicate<string?>>.Ignored, out orderIdOut)).Returns(false);
            A.CallTo(() => _orderService.OrderExist(A<int>.Ignored)).Returns(true);
            decimal orderValue = 20.5m;
            A.CallTo(() => _orderService.GetOrderValue(A<int>.Ignored)).Returns(orderValue);
            A.CallTo(() => _orderService.GetPaymentOptionId("Gotówka przy odbiorze")).Returns(2);
            A.CallTo(() => _orderService.GetOrderPaymentOptionId(A<int>.Ignored)).Returns(2);
            A.CallTo(() => _orderService.GetStatusId("Nowe")).Returns(1);
            A.CallTo(() => _orderService.GetOrderStatusId(A<int>.Ignored)).Returns(2);

            //Act
            await consoleFunctions.PassOrderToWarehouse();

            //Assert
            Assert.Contains("Error: Cannot change given order status, because the order status is not Nowe.", strWriter.ToString());
            Assert.DoesNotContain($"Order status with id {orderIdOut} has been changed", strWriter.ToString());
            Assert.DoesNotContain($"Error: Could not change order status with id {orderIdOut}.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_PassOrderToWarehouse_NewOrderStatusDoNotExist_WriteErrorMessage()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? orderIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the order that you want to move to warehouse.", A<Predicate<string?>>.Ignored, out orderIdOut)).Returns(false);
            A.CallTo(() => _orderService.OrderExist(A<int>.Ignored)).Returns(true);
            decimal orderValue = 20.5m;
            A.CallTo(() => _orderService.GetOrderValue(A<int>.Ignored)).Returns(orderValue);
            A.CallTo(() => _orderService.GetPaymentOptionId("Gotówka przy odbiorze")).Returns(2);
            A.CallTo(() => _orderService.GetOrderPaymentOptionId(A<int>.Ignored)).Returns(2);
            A.CallTo(() => _orderService.GetStatusId("Nowe")).Returns(1);
            A.CallTo(() => _orderService.GetOrderStatusId(A<int>.Ignored)).Returns(1);
            A.CallTo(() => _orderService.GetStatusId("W magazynie")).Returns(0);

            //Act
            await consoleFunctions.PassOrderToWarehouse();

            //Assert
            Assert.Contains("Error: Could not find status with the name W magazynie", strWriter.ToString());
            Assert.DoesNotContain($"Order status with id {orderIdOut} has been changed", strWriter.ToString());
            Assert.DoesNotContain($"Error: Could not change order status with id {orderIdOut}.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_PassOrderToWarehouse_CouldNotChangeStatus_WriteErrorMessage()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? orderIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the order that you want to move to warehouse.", A<Predicate<string?>>.Ignored, out orderIdOut)).Returns(false);
            A.CallTo(() => _orderService.OrderExist(A<int>.Ignored)).Returns(true);
            decimal orderValue = 20.5m;
            A.CallTo(() => _orderService.GetOrderValue(A<int>.Ignored)).Returns(orderValue);
            A.CallTo(() => _orderService.GetPaymentOptionId("Gotówka przy odbiorze")).Returns(2);
            A.CallTo(() => _orderService.GetOrderPaymentOptionId(A<int>.Ignored)).Returns(2);
            A.CallTo(() => _orderService.GetStatusId("Nowe")).Returns(1);
            A.CallTo(() => _orderService.GetOrderStatusId(A<int>.Ignored)).Returns(1);
            A.CallTo(() => _orderService.GetStatusId("W magazynie")).Returns(2);
            A.CallTo(() => _orderService.ModifyOrder(A<ModifyOrder>.Ignored)).Returns(false);

            //Act
            await consoleFunctions.PassOrderToWarehouse();

            //Assert
            Assert.Contains($"Error: Could not change order status with id {orderIdOut}.", strWriter.ToString());
            Assert.DoesNotContain($"Order status with id {orderIdOut} has been changed to W magazynie.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_ModifyProduct_SuccessfullyChanged_WriteSuccessMessage()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? productIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the product that you want to modify.", A<Predicate<string?>>.Ignored, out productIdOut)).Returns(false);
            A.CallTo(() => _productService.ProductExist(A<int>.Ignored)).Returns(true);
            var productInfo = new GetProductInfo
            {
                Name = "name",
                Ean = "1233"
            };
            A.CallTo(() => _productService.GetProductInfo(A<int>.Ignored)).Returns(productInfo);
            string? newName = "newName";
            A.CallTo(() => _consoleUtils.GetParameter("Pass new product name or nothing if you do not want to change it.", A<Predicate<string?>>.Ignored, out newName)).Returns(false);
            string? newEan = "1244";
            A.CallTo(() => _consoleUtils.GetParameter("Pass new product ean or nothing if you do not want to change it.", A<Predicate<string?>>.Ignored, out newEan)).Returns(false);
            A.CallTo(() => _productService.ProductExist(newEan)).Returns(false);
            A.CallTo(() => _productService.ModifyProduct(A<ModifyProduct>.Ignored)).Returns(true);

            //Act
            await consoleFunctions.ModifyProduct();

            //Assert
            Assert.Contains($"Successfully changed the product with id {productIdOut}.", strWriter.ToString());
            Assert.DoesNotContain("Error:", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_ModifyProduct_ProductDoNotExist_WriteErrorMessage()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? productIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the product that you want to modify.", A<Predicate<string?>>.Ignored, out productIdOut)).Returns(false);
            A.CallTo(() => _productService.ProductExist(A<int>.Ignored)).Returns(false);

            //Act
            await consoleFunctions.ModifyProduct();

            //Assert
            Assert.Contains($"Error: The product with id {productIdOut} do not exists.", strWriter.ToString());
            Assert.DoesNotContain($"Successfully changed the product with id {productIdOut}.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_ModifyProduct_CouldNotFindProductInfo_WriteErrorMessage()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? productIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the product that you want to modify.", A<Predicate<string?>>.Ignored, out productIdOut)).Returns(false);
            A.CallTo(() => _productService.ProductExist(A<int>.Ignored)).Returns(true);
            GetProductInfo? productInfo = null;
            A.CallTo(() => _productService.GetProductInfo(A<int>.Ignored)).Returns(productInfo);

            //Act
            await consoleFunctions.ModifyProduct();

            //Assert
            Assert.Contains("Error: Could not find product information form database.", strWriter.ToString());
            Assert.DoesNotContain($"Successfully changed the product with id {productIdOut}.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_ModifyProduct_NewEanAlreadyExist_WriteErrorMessage()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? productIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the product that you want to modify.", A<Predicate<string?>>.Ignored, out productIdOut)).Returns(false);
            A.CallTo(() => _productService.ProductExist(A<int>.Ignored)).Returns(true);
            var productInfo = new GetProductInfo
            {
                Name = "name",
                Ean = "1233"
            };
            A.CallTo(() => _productService.GetProductInfo(A<int>.Ignored)).Returns(productInfo);
            string? newName = "newName";
            A.CallTo(() => _consoleUtils.GetParameter("Pass new product name or nothing if you do not want to change it.", A<Predicate<string?>>.Ignored, out newName)).Returns(false);
            string? newEan = "1244";
            A.CallTo(() => _consoleUtils.GetParameter("Pass new product ean or nothing if you do not want to change it.", A<Predicate<string?>>.Ignored, out newEan)).Returns(false);
            A.CallTo(() => _productService.ProductExist(newEan)).Returns(true);

            //Act
            await consoleFunctions.ModifyProduct();

            //Assert
            Assert.Contains("Error: Product with this ean already exist.", strWriter.ToString());
            Assert.DoesNotContain($"Successfully changed the product with id {productIdOut}.", strWriter.ToString());
        }
        [Fact]
        public async Task ConsoleFunctions_ModifyProduct_FailsToChange_WriteErrorMessage()
        {
            //Arrange
            ConsoleFunctions consoleFunctions = new(_consoleUtils, _orderService, _productService);
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string? productIdOut = "1";
            A.CallTo(() => _consoleUtils.GetParameter("Please write the id of the product that you want to modify.", A<Predicate<string?>>.Ignored, out productIdOut)).Returns(false);
            A.CallTo(() => _productService.ProductExist(A<int>.Ignored)).Returns(true);
            var productInfo = new GetProductInfo
            {
                Name = "name",
                Ean = "1233"
            };
            A.CallTo(() => _productService.GetProductInfo(A<int>.Ignored)).Returns(productInfo);
            string? newName = "newName";
            A.CallTo(() => _consoleUtils.GetParameter("Pass new product name or nothing if you do not want to change it.", A<Predicate<string?>>.Ignored, out newName)).Returns(false);
            string? newEan = "1244";
            A.CallTo(() => _consoleUtils.GetParameter("Pass new product ean or nothing if you do not want to change it.", A<Predicate<string?>>.Ignored, out newEan)).Returns(false);
            A.CallTo(() => _productService.ProductExist(newEan)).Returns(false);
            A.CallTo(() => _productService.ModifyProduct(A<ModifyProduct>.Ignored)).Returns(false);

            //Act
            await consoleFunctions.ModifyProduct();

            //Assert
            Assert.DoesNotContain($"Successfully changed the product with id {productIdOut}.", strWriter.ToString());
            Assert.Contains("Error: Could not change product.", strWriter.ToString());
        }
    }
}
