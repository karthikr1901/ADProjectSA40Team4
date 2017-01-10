package com.example.student.adprojectsa40team4;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;

/**
 * Created by student on 13/9/15.
 */
public class AdjustmentVoucherInfo {

    private static final long serialVersionUID = -4444602266741220062L;
    private static ArrayList<RequisitionFormInfo> parts = null;
    public static String wcfUrl = ConfigurationManager.wcfUrl;
    public static ArrayList<AdjustmentVoucherNumber> list = new ArrayList<AdjustmentVoucherNumber>();

    public String voucherNumber, Description, Remark, UOM;
    public double Amount, TotalPrice, UnitOfPrice;
    public int Balance;

    public AdjustmentVoucherInfo(double Amount, int Balance, String Description, String Remark, double TotalPrice, String UOM, double UnitOfPrice) {
        this.Amount = Amount;
        this.Balance = Balance;
        this.Description = Description;
        this.Remark = Remark;
        this.TotalPrice = TotalPrice;
        this.UOM = UOM;
        this.UnitOfPrice = UnitOfPrice;
    }

    public AdjustmentVoucherInfo(String voucherNumber) {
        this.voucherNumber = voucherNumber;
    }

    public static ArrayList<AdjustmentVoucherInfo> getAdjustmentListMobile(String adjID) {
        ArrayList<AdjustmentVoucherInfo> arrResult = new ArrayList<AdjustmentVoucherInfo>();
        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/getAdjustmentListMobile/%s", wcfUrl, adjID));
            for (int i = 0; i < a.length(); i++) {
                JSONObject obj = a.getJSONObject(i);
                arrResult.add(new AdjustmentVoucherInfo(obj.getDouble("Amount"), obj.getInt("Balance"), obj.getString("Description"), obj.getString("Remark"), obj.getDouble("TotalPrice"), obj.getString("UOM"), obj.getDouble("UnitOfPrice")));
            }
        } catch (Exception e) {
        }
        return arrResult;
    }

    public static ArrayList<AdjustmentVoucherNumber> GetAdjVoucherNumberMobile(String roleID) {
        ArrayList<AdjustmentVoucherNumber> arrResult = new ArrayList<AdjustmentVoucherNumber>();
        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/GetVoucherNumberMobile/%s", wcfUrl, roleID));
            for (int i = 0; i < a.length(); i++) {
                JSONObject obj = a.getJSONObject(i);
                arrResult.add(new AdjustmentVoucherNumber(obj.getString("voucherNumber").toString()));
                String temp = obj.getString("voucherNumber").toString();
                list.add(new AdjustmentVoucherNumber(temp));
            }
        } catch (Exception e) {
        }
        return arrResult;
    }

    public static ReturnMessage ApproveAdjustmentVoucher(String adjID, String empID) {
        String urlStr = String.format("%s/approvedAdjustmentListMobile/%s/%s", wcfUrl, adjID, empID);
        ReturnMessage Rmsg = null;
        try {
            JSONObject obj = JSONParser.getJSONFromUrl(String.format("%s/approvedAdjustmentListMobile/%s/%s", wcfUrl, adjID, empID));

            if (obj != null) {
                Rmsg = new ReturnMessage(obj.getString("RMessage"));
            }

        } catch (Exception e) {
        }
        return  Rmsg;
    }

    public static class AdjustmentVoucherNumber {

        private String vNumber;

        public AdjustmentVoucherNumber(String vNumber) {
            this.vNumber = vNumber;
        }

        @Override
        public String toString() {
            return vNumber;
        }
    }

    public static  class ReturnMessage {
        public  String msg;

        public ReturnMessage(String msg) {
            this.msg = msg;
        }
    }
}


