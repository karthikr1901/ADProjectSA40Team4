package com.example.student.adprojectsa40team4;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;

/**
 * Created by student on 10/9/15.
 */
public class RequestModel extends HashMap<String, String> {
    private static final long serialVersionUID = -4444602266741220062L;
    //public static String wcfUrl = "http://10.10.3.60/LogicUniversityWCFService/Service.svc";
    public static String wcfUrl = ConfigurationManager.wcfUrl;
    //public static String RequestID = "";

    public static String GetRequestIDByEmployeeID(String EmployeeID) {
     String RequestID="";
        try {
            JSONObject obj = JSONParser.getJSONFromUrl(String.format("%s/GetRequestIDByEmployeeID/%s", wcfUrl, EmployeeID));
            RequestID =  obj.getString("RequestID");

        } catch (Exception e) {
            Log.e("getProduct", "JSON error");
        }
        return RequestID;
    }

    public static ArrayList<RequestItems> GetRequestItems(String RequestID) {
        ArrayList<RequestItems> reqlist = new ArrayList<RequestItems>();

        JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/GetRequestItems/%s", wcfUrl, RequestID));
        try {
            for (int i =0; i<a.length(); i++) {

                JSONObject obj = a.getJSONObject(i);
                reqlist.add(new RequestItems(obj.getString("Description"),Integer.parseInt(obj.getString("Quantity")),obj.getString("UnitOfMeasurement")));

            }
        } catch (Exception e) {
            Log.e("GetEmployeeNameList", "JSONArray error");
        }
        return reqlist;
    }

    public static String UpdateRequestStatusToApprove(String RequestID, String Status, String ApprovedByEmployeeID, String Remark) {
        String RMessage="";
        try {
            JSONObject obj = JSONParser.getJSONFromUrl(String.format("%s/UpdateRequestStatusToApprove/%s/%s/%s/%s", wcfUrl, RequestID, Status, ApprovedByEmployeeID, Remark));
            RMessage =  obj.getString("RMessage").toString();

        } catch (Exception e) {
            Log.e("UpdateRequest", "JSON error");
        }
        return RMessage;
    }

    public static class RequestItems {

        public String Description;
        public int Quantity;
        public String UnitOfMeasurement;

        public RequestItems(String Description,int Quantity, String UnitOfMeasurement) {
            this.Description = Description;
            this.Quantity = Quantity;
            this.UnitOfMeasurement = UnitOfMeasurement;
        }
    }

//    public static class RequestID {
//
//        private int rID;
//
//        public RequestID(int rID) {
//            this.rID = rID;
//        }
//
//        @Override
//        public String toString() {
//            return String.valueOf(rID);
//        }
//    }
}
