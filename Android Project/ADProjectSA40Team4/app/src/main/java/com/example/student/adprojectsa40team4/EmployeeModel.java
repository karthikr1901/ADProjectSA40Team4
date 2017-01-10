package com.example.student.adprojectsa40team4;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;

/**
 * Created by student on 9/9/15.
 */
public class EmployeeModel extends HashMap<String, String> {
    private static final long serialVersionUID = -4444602266741220062L;
//    public static String wcfUrl = "http://10.10.3.60/LogicUniversityWCFService/Service.svc";
    public static String wcfUrl = ConfigurationManager.wcfUrl;
    public static ArrayList<EmployeeID> eIdlist;
    public static ArrayList<EmployeeName> list;

    public static String EmployeeID = "";

    public static ArrayList<EmployeeName> GetEmployeeNameList() {

        list = new ArrayList<EmployeeName>();
        eIdlist = new ArrayList<EmployeeID>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/GetEmployeeNameList/%s", wcfUrl, LogInModel.empDept));
        try {
            for (int i =0; i<a.length(); i++) {

                JSONObject obj = a.getJSONObject(i);
                list.add(new EmployeeName(obj.getString("EmployeeName")));
                eIdlist.add(new EmployeeID(obj.getString("EmployeeID")));
            }
        } catch (Exception e) {
            Log.e("GetEmployeeNameList", "JSONArray error");
        }
        return list;
    }

    public static EmployeeName GetRepresentative(String DepartmentID) {
        EmployeeName emp=null;

        try {
            JSONObject obj = JSONParser.getJSONFromUrl(String.format("%s/GetRepresentative/%s", wcfUrl, DepartmentID));
            emp =  new EmployeeName(obj.getString("EmployeeName").toString());
            EmployeeID = obj.getString("EmployeeID").toString();

        } catch (Exception e) {
            Log.e("GetRepresentative", "JSON error");
        }
        return emp;
    }

    public static String ApproveNewRepresentativeMobile(String EmployeeID, String DepartmentID) {
        String RMessage="";
        try {
            JSONObject obj = JSONParser.getJSONFromUrl(String.format("%s/appointNewRepresentativeMobile/%s/%s", wcfUrl, EmployeeID, DepartmentID));
            RMessage =  obj.getString("RMessage").toString();

        } catch (Exception e) {
            Log.e("ApproveRepresentative", "JSON error");
        }
        return RMessage;
    }

//    public static String GetEmployeeIDByEmployeeName(String EmployeeName) {
//        String EmployeeID="";
//        JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/GetEmployeeIDByEmployeeName/%s", wcfUrl, EmployeeName));
//        try {
//            for (int i =0; i<a.length(); i++) {
//
//                JSONObject obj = a.getJSONObject(i);
//
//                EmployeeID = obj.getString("EmployeeID");
//
//            }
//        } catch (Exception e) {
//            Log.e("GetEmployeeNameList", "JSONArray error");
//        }
//        return EmployeeID;
//    }

    public static class EmployeeName {

        private String eName;

        public EmployeeName(String eName) {
            this.eName = eName;
        }

        @Override
        public String toString() {
            return eName;
        }
    }

    public static class EmployeeID {

        private String eId;

        public EmployeeID(String eId) {
            this.eId = eId;
        }

        @Override
        public String toString() {
            return eId;
        }
    }
}
