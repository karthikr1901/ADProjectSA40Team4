package com.example.student.adprojectsa40team4;

import org.json.JSONObject;

/**
 * Created by student on 8/9/15.
 */
public class LogInModel {
    private static final long serialVersionUID = -4444602266741220062L;

    //public static String wcfUrl = "http://10.10.3.48//LogicUniversityWCFService/Service.svc";
    public static String wcfUrl = ConfigurationManager.wcfUrl;

    public static String empName,empEmail,empDept;
    public static int empRole,empID;

    public LogInModel(int empRole,int empID, String empName,String empEmail,String empDept) {
        LogInModel.empRole = empRole;
        LogInModel.empID = empID;
        LogInModel.empName = empName;
        LogInModel.empEmail = empEmail;
        LogInModel.empDept = empDept;
    }

    public static LogInModel GetAuthenticateUser(String name,String password){
        LogInModel objLogin = null;
        String urlStr = String.format( "%s/GetAuthenticateUser/%s/%s",wcfUrl,name,password);

        try {
            JSONObject obj = JSONParser.getJSONFromUrl(String.format( "%s/GetAuthenticateUser/%s/%s",wcfUrl,name,password));
            objLogin = new LogInModel(Integer.parseInt(obj.getString("RoleID")),Integer.parseInt(obj.getString("EmployeeID")),obj.getString("EmployeeName"),obj.getString("EmployeeEmail"),obj.getString("DepartmentID"));
        } catch (Exception e) {
        }
        return  objLogin;
    }

    public static Integer GetLowLevelStockQty(){
        LogInModel objLogin = null;
        String urlStr = String.format("%s/getLowLevelStockQty",wcfUrl);
        String result="";
        Integer res=0;
        String substr;
        try {
             result = JSONParser.getStream(urlStr);
            //res = Integer.parseInt(result.toString());
             substr=result.substring(result.indexOf("\n"));
            result = result.replace(substr,"");
             res = Integer.parseInt(result);

        } catch (Exception e) {
        }
        return  res;
    }
}
