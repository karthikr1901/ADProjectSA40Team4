package com.example.student.adprojectsa40team4;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;

/**
 * Created by student on 10/9/15.
 */
public class EmployeeInfo {

    private static final long serialVersionUID = -4444602266741220062L;
    //public static String wcfUrl = "http://10.10.3.48//LogicUniversityWCFService/Service.svc";
    public static String wcfUrl = ConfigurationManager.wcfUrl;
    public  String  EmployeeID,RoleID,EmployeePassword;
    public  String DepartmentID,EmployeeAddress,EmployeeContactNo,EmployeeEmail,EmployeeName;


    public EmployeeInfo(String DepartmentID, String EmployeeAddress,String EmployeeContactNo,String EmployeeEmail,String EmployeeID,String EmployeeName,String EmployeePassword,String RoleID) {
        this.DepartmentID = DepartmentID;
        this.EmployeeAddress = EmployeeAddress;
        this.EmployeeContactNo = EmployeeContactNo;
        this.EmployeeEmail = EmployeeEmail;
        this.EmployeeID = EmployeeID;
        this.EmployeeName = EmployeeName;
        this.EmployeePassword = EmployeePassword;
        this.RoleID = RoleID;
    }

    public EmployeeInfo(String DepartmentID,String EmployeeName,String EmployeeEmail) {
        this.DepartmentID = DepartmentID;
        this.EmployeeName = EmployeeName;
        this.EmployeeEmail = EmployeeEmail;
    }

    public static EmployeeInfo GetEmployeeIDByEmployeeName(String empName){
        EmployeeInfo objEmp = null;
        try {


            JSONObject obj = JSONParser.getJSONFromUrl(String.format("http://10.10.3.48//LogicUniversityWCFService/Service.svc/GetEmployeeIDByEmployeeName/%s",empName));
            if (obj != null)
            objEmp = new EmployeeInfo(obj.getString("DepartmentID"),obj.getString("EmployeeAddress"),obj.getString("EmployeeContactNo"),
                    obj.getString("EmployeeEmail"),obj.getString("EmployeeID"),obj.getString("EmployeeName"),obj.getString("EmployeePassword"),obj.getString("RoleID"));
        } catch (Exception e) {
            e.printStackTrace();
        }
        return  objEmp;
    }

    public static ArrayList<EmployeeInfo> GetDeptRepListForCollectionPoint(String pointName){
        ArrayList<EmployeeInfo> empRepList = new ArrayList<EmployeeInfo>();
        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/GetDeptRepListForCollectionPoint/%s", wcfUrl, pointName));
            for (int i=0; i<a.length(); i++) {
                JSONObject obj = a.getJSONObject(i);
                empRepList.add(new EmployeeInfo(obj.getString("DepartmentID"),obj.getString("EmployeeName"),obj.getString("EmployeeEmail")));
            }
        } catch (Exception e) {

        }
        return empRepList;
    }
}
