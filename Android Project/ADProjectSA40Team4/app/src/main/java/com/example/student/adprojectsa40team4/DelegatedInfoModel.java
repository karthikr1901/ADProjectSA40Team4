package com.example.student.adprojectsa40team4;

import android.util.Log;

import org.json.JSONObject;

import java.util.HashMap;

/**
 * Created by student on 13/9/15.
 */
public class DelegatedInfoModel extends HashMap<String, String> {
    private static final long serialVersionUID = -4444602266741220062L;
    //public static String wcfUrl = "http://10.10.3.60/LogicUniversityWCFService/Service.svc";
    public static String wcfUrl = ConfigurationManager.wcfUrl;
    public static CancellationOfAuthority cautho;
    public  static String DInfoID;

    public static String delegateAuthority(String EmployeeID, String FromDate, String ToDate) {
        String RCode="";
        try {
            JSONObject obj = JSONParser.getJSONFromUrl(String.format("%s/delegateAuthorityMobile/%s/%s/%s", wcfUrl, EmployeeID, FromDate, ToDate));
            RCode =  obj.getString("RMessage");

        } catch (Exception e) {
            Log.e("delegateAuthority", "JSON error");
        }
        return RCode;
    }

    public static CancellationOfAuthority GetDelegatedInfo(String DepartmentID) {

        try {
            JSONObject obj = JSONParser.getJSONFromUrl(String.format("%s/GetDelegatedInfo/%s", wcfUrl, DepartmentID));
            cautho = new CancellationOfAuthority(Integer.parseInt(obj.getString("delicatedInfoID")),obj.getString("employeeName"));
            DInfoID = obj.getString("delicatedInfoID");

        } catch (Exception e) {
            Log.e("GetDelegatedInfo", "JSON error");
        }
        return cautho;
    }

    public static String DeleteDelegatedInfo(String DelicatedInfoID) {
        String RCode="";
        try {
            JSONObject obj = JSONParser.getJSONFromUrl(String.format("%s/DeleteDelegatedInfo/%s", wcfUrl, DelicatedInfoID));
            RCode =  obj.getString("RMessage");

        } catch (Exception e) {
            Log.e("GetDeleteDelegatedInfo", "JSON error");
        }
        return RCode;
    }

    public static class CancellationOfAuthority {

        public int delicatedInfoID;
        public String employeeName;

        public CancellationOfAuthority(int delicatedInfoID,String EmployeeName) {
            this.delicatedInfoID = delicatedInfoID;
            this.employeeName = EmployeeName;
        }

        @Override
        public String toString() {
            return employeeName;
        }
    }
}
