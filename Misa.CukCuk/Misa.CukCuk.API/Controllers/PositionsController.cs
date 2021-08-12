using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Api.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {


        /// <summary>
        /// API hiển thị tất cả các phòng ban
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPositions()
        {
            //Truy cập vào database
            //1. Khởi tạo thông tin kết nối database
            var connectionString = "Host= 47.241.69.179;" +
                    "Database = MISA.CukCuk_Demo_NVMANH;" +
                    "User Id = dev;" +
                    "Password = 12345678;";
            //2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);


            //3. Lấy dữ liệu
            var sqlCommand = "SELECT * FROM Position";
            var positions = dbConnection.Query<object>(sqlCommand);

            //4. Trả về cho client
             var response = StatusCode(200, positions);
            return response;
        }

        /// <summary>
        /// API hiển thị phòng ban theo id
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        [HttpGet("{positionId}")]
        public IActionResult GetPositionById(Guid positionId)
        {
            //Truy cập vào database
            //1. Khởi tạo thông tin kết nối database
            var connectionString = "Host= 47.241.69.179;" +
                    "Database = MISA.CukCuk_Demo_NVMANH;" +
                    "User Id = dev;" +
                    "Password = 12345678;";
            //2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            //3. Lấy dữ liệu
            var sqlCommand = $"SELECT * FROM Position WHERE positionId= @positionIdParam";

            //De trach loi SQL Injection           
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@positionIdParam", positionId);

            var position = dbConnection.QueryFirstOrDefault<Position>(sqlCommand, param: parameters);

            //4. Trả về cho client
            var response = StatusCode(200, position);
            return response;

        }

        /// <summary>
        /// API thêm mới 1 bản ghi nhân viên
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertPosition(Position position)
        {
            //Truy cập vào database
            //1. Khởi tạo thông tin kết nối database
            var connectionString = "Host= 47.241.69.179;" +
                    "Database = MISA.CukCuk_Demo_NVMANH;" +
                    "User Id = dev;" +
                    "Password = 12345678;";
            //2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            //Khai báo dyanamicParam:
            var dyanamicParam = new DynamicParameters();

            //3. Thêm dữ liệu vào trong database
            var columnsName = string.Empty;

            var columnsParam = string.Empty;

            //Đọc từng property của object:
            var properties = position.GetType().GetProperties();
            foreach (var prop in properties)
            {
                //Lấy tên của prop:
                var propName = prop.Name;

                //Lấy value của prop
                var propValue = prop.GetValue(position);

                //Lấy kiểu dữ liệu của prop
                var propType = prop.PropertyType;

                //Thêm param tương ứng với mỗi property của đối tượng
                dyanamicParam.Add($"@{propName}", propValue);

                columnsName += $"{propName},";

                columnsParam += $"@{propName},";

            }

            columnsName = columnsName.Remove(columnsName.Length - 1, 1);

            columnsParam = columnsParam.Remove(columnsParam.Length - 1, 1);

            var sqlCommand = $"INSERT INTO Position({columnsName}) VALUES({columnsParam})";

            var rowsEffects = dbConnection.Execute(sqlCommand, param: dyanamicParam);

            //4. Trả về cho client
            var response = StatusCode(200, rowsEffects);
            return response;

        }

        /// <summary>
        /// Xóa phòng ban
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        [HttpDelete("{positionId}")]
        public IActionResult DeletePositionById(Guid positionId)
        {
            //Truy cập vào database
            //1. Khởi tạo thông tin kết nối database
            var connectionString = "Host= 47.241.69.179;" +
                    "Database = MISA.CukCuk_Demo_NVMANH;" +
                    "User Id = dev;" +
                    "Password = 12345678;";
            //2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            //3. Lấy dữ liệu
            var sqlCommand = $"DELETE FROM Position WHERE PositionId= @positionIdParam";

            //De trach loi SQL Injection           
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@positionIdParam", positionId);

            var position = dbConnection.QueryFirstOrDefault<Position>(sqlCommand, param: parameters);

            //4. Trả về cho client
            var response = StatusCode(200, position);
            return response;
        }

        /// <summary>
        /// Sửa 
        /// </summary>
        [HttpPut("{positionId}")]
        public IActionResult Updateposition(Guid positionId, Position position)
        {

            //Truy cập vào database
            //1. Khởi tạo thông tin kết nối database
            var connectionString = "Host= 47.241.69.179;" +
                    "Database = MISA.CukCuk_Demo_NVMANH;" +
                    "User Id = dev;" +
                    "Password = 12345678;";

            //2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            //Khai báo dyanamicParam:
            var dyanamicParam = new DynamicParameters();

            //3. Thêm dữ liệu vào trong database
            var columnsUpadateParam = string.Empty;

            //Đọc từng property của object:         
            var properties = position.GetType().GetProperties();
            foreach (var prop in properties)
            {
                //Lấy tên của prop:
                var propName = prop.Name;

                //Lấy value của prop
                var propValue = prop.GetValue(position);

                //Lấy kiểu dữ liệu của prop
                var propType = prop.PropertyType;

                //Thêm param tương ứng với mỗi property của đối tượng
                dyanamicParam.Add($"@{propName}", propValue);

                columnsUpadateParam += $"{propName} = '@{ propName}' ,";

            }

            columnsUpadateParam = columnsUpadateParam.Remove(columnsUpadateParam.Length - 1, 1);

            var sqlCommand = $"UPDATE Position SET {columnsUpadateParam} WHERE PositionId = @positionId";
            dyanamicParam.Add("@positionId", positionId);

            var rowsEffects = dbConnection.Execute(sqlCommand, param: dyanamicParam);

            //4. Trả về cho client
            var response = StatusCode(200, rowsEffects);
            return response;
        }


    }

}

