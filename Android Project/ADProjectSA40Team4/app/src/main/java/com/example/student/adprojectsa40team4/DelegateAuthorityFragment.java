package com.example.student.adprojectsa40team4;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.DatePickerDialog;
import android.app.TimePickerDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.net.Uri;
import android.os.Bundle;
import android.app.Fragment;
import android.os.StrictMode;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.inputmethod.InputMethodManager;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.TimePicker;
import android.widget.Toast;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.Calendar;
import java.util.Date;


/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link DelegateAuthorityFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link DelegateAuthorityFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class DelegateAuthorityFragment extends Fragment implements View.OnClickListener{
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private OnFragmentInteractionListener mListener;
    Spinner spinner;

    Button btnSubmit;
    EditText etFromDateTime, etToDateTime;
    String RMessage, FromDate, ToDate;
    String[] empName;
    int index;
    String empID;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment DelegateAuthorityFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static DelegateAuthorityFragment newInstance(String param1, String param2) {
        DelegateAuthorityFragment fragment = new DelegateAuthorityFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    public DelegateAuthorityFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            mParam1 = getArguments().getString(ARG_PARAM1);
            mParam2 = getArguments().getString(ARG_PARAM2);
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View v = inflater.inflate(R.layout.fragment_delegate_authority, container, false);

        ArrayList<EmployeeModel.EmployeeName> namelist = new ArrayList<EmployeeModel.EmployeeName>();
        namelist = EmployeeModel.GetEmployeeNameList();

        empName = new String[namelist.size()];

        for (int i = 0; i < namelist.size(); i++) {
            empName[i] = namelist.get(i).toString();
        }

        spinner = (Spinner) v.findViewById(R.id.spnEmployee);
        ArrayAdapter<String> LTRadapter = new ArrayAdapter<String>(this.getActivity(), android.R.layout.simple_spinner_item, empName);
        LTRadapter.setDropDownViewResource(android.R.layout.simple_dropdown_item_1line);
        spinner.setAdapter(LTRadapter);

        btnSubmit = (Button) v.findViewById(R.id.btnSubmit);
        etFromDateTime = (EditText) v.findViewById(R.id.etFromDate);
        etFromDateTime.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                // TODO Auto-generated method stub
                Calendar myCalendar = Calendar.getInstance();
                int year = myCalendar.get(Calendar.YEAR);
                int month = myCalendar.get(Calendar.MONTH);
                int day = myCalendar.get(Calendar.DAY_OF_MONTH);

//                int hour = myCalendar.get(Calendar.HOUR_OF_DAY);
//                int minute = myCalendar.get(Calendar.MINUTE);
//                TimePickerDialog mTimePicker;
//                mTimePicker = new TimePickerDialog(getActivity(), new TimePickerDialog.OnTimeSetListener() {
//                    @Override
//                    public void onTimeSet(TimePicker timePicker, int selectedHour, int selectedMinute) {
//                        etFromDateTime.setText("" + selectedHour + ":" + selectedMinute);
//                    }
//
//
//                }, hour, minute, true);//Yes 24 hour time

                DatePickerDialog mDatePicker;
                mDatePicker = new DatePickerDialog(getActivity(), new DatePickerDialog.OnDateSetListener() {
                    public void onDateSet(DatePicker datepicker, int selectedyear, int selectedmonth, int selectedday) {
                        // TODO Auto-generated method stub
                    /*      Your code   to get date and time    */
                        selectedmonth = selectedmonth + 1;
                        etFromDateTime.setText("" + selectedday + "-" + selectedmonth + "-" + selectedyear);
                        FromDate = "" + selectedmonth + "-" + selectedday + "-" + selectedyear;
                    }
                }, year, month, day);

                mDatePicker.setTitle("Select Date");
                mDatePicker.show();

            }
        });

        etToDateTime = (EditText) v.findViewById(R.id.etToDate);
        etToDateTime.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                // TODO Auto-generated method stub
                Calendar myCalendar = Calendar.getInstance();
                int year = myCalendar.get(Calendar.YEAR);
                int month = myCalendar.get(Calendar.MONTH);
                int day = myCalendar.get(Calendar.DAY_OF_MONTH);

//                int hour = myCalendar.get(Calendar.HOUR_OF_DAY);
//                int minute = myCalendar.get(Calendar.MINUTE);
//                TimePickerDialog mTimePicker;
//                mTimePicker = new TimePickerDialog(getActivity(), new TimePickerDialog.OnTimeSetListener() {
//                    @Override
//                    public void onTimeSet(TimePicker timePicker, int selectedHour, int selectedMinute) {
//                        etFromDateTime.setText("" + selectedHour + ":" + selectedMinute);
//                    }
//
//
//                }, hour, minute, true);//Yes 24 hour time

                DatePickerDialog mDatePicker;
                mDatePicker = new DatePickerDialog(getActivity(), new DatePickerDialog.OnDateSetListener() {
                    public void onDateSet(DatePicker datepicker, int selectedyear, int selectedmonth, int selectedday) {
                        // TODO Auto-generated method stub
                    /*      Your code   to get date and time    */
                        selectedmonth = selectedmonth + 1;
                        etToDateTime.setText("" + selectedday + "-" + selectedmonth + "-" + selectedyear);
                        ToDate = "" + selectedmonth + "-" + selectedday + "-" + selectedyear;
                    }
                }, year, month, day);

                mDatePicker.setTitle("Select Date");
                mDatePicker.show();

            }
        });

        btnSubmit.setOnClickListener(this);
        return v;
    }

    @Override
    public void onClick(View v) {
        if (v == btnSubmit) {
            String[] separated = FromDate.split("-");

            String strFDate, strTDate;
            DateFormat dateFormat = new SimpleDateFormat("dd-MM-yyyy");
            //get current date time with Date()
            //Date date = new Date();

            //get current date time with Calendar()
            Calendar cal = Calendar.getInstance();
            strFDate = dateFormat.format(cal.getTime());

            strTDate =  separated[1] + "-" + separated[0]  + "-" + separated[2];

            boolean r = CheckDates(strFDate,strTDate);
            if (r==false) {
                Toast.makeText(getActivity(), "Invalid From Date", Toast.LENGTH_SHORT).show();
                etFromDateTime.requestFocus();
                return;
            }

            separated = ToDate.split("-");
            strTDate =  separated[1] + "-" + separated[0]  + "-" + separated[2];
            r = CheckDates(strFDate, strTDate);
            if (r==false) {
                Toast.makeText(getActivity(), "Invalid To Date", Toast.LENGTH_SHORT).show();
                etToDateTime.requestFocus();
                return;
            }

            separated = FromDate.split("-");
            strFDate =  separated[1] + "-" + separated[0]  + "-" + separated[2];
            r = CheckDates(strFDate,strTDate);
            if (r==false) {
                Toast.makeText(getActivity(), "Invalid To Date", Toast.LENGTH_SHORT).show();
                etToDateTime.requestFocus();
                return;
            }


            AlertDialog.Builder builder = new AlertDialog.Builder(v.getContext());
            builder
                    .setTitle("Delegate Authority")
                    .setMessage("Are you sure you want to delegate this person?")
                    .setPositiveButton("ACCEPT", new DialogInterface.OnClickListener() {
                        public void onClick(DialogInterface dialog, int which) {

                            for (int i = 0; i < empName.length; i++) {
                                if (empName[i] == spinner.getSelectedItem().toString()) {
                                    index = i;
                                }
                            }

                            empID = EmployeeModel.eIdlist.get(index).toString();

                            StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);

                            RMessage = DelegatedInfoModel.delegateAuthority(empID, FromDate, ToDate);

                            Toast.makeText(getActivity().getApplicationContext(), RMessage, Toast.LENGTH_SHORT).show();

                            Refresh();
//                            android.app.FragmentManager fragmentManager = getFragmentManager();
//                            ARRFragment fragment=new ARRFragment();
//
//                            fragmentManager.beginTransaction()
//                                    .replace(R.id.container, fragment)
//                                    .commit();
                        }
                    })
                    .setNegativeButton("DECLINE", null)						//Do nothing on no
                    .show();
        }

        return;
    }

    public void Refresh(){

        ArrayAdapter<String> LTRadapter = new ArrayAdapter<String>(this.getActivity(), android.R.layout.simple_spinner_item, empName);
        LTRadapter.setDropDownViewResource(android.R.layout.simple_dropdown_item_1line);
        spinner.setAdapter(LTRadapter);

        etFromDateTime.setText("");
        etToDateTime.setText("");

        spinner.setFocusable(true);
        spinner.setFocusableInTouchMode(true);
        spinner.requestFocus();

    }

    public static boolean CheckDates(String startDate, String endDate) {

        SimpleDateFormat dfDate = new SimpleDateFormat("dd-MM-yyyy");

        boolean b = false;

        try {
            if (dfDate.parse(startDate).before(dfDate.parse(endDate))) {
                b = true;  // If start date is before end date.
            } else if (dfDate.parse(startDate).equals(dfDate.parse(endDate))) {
                b = true;  // If two dates are equal.
            } else {
                b = false; // If start date is after the end date.
            }
        } catch (ParseException e) {
            e.printStackTrace();
        }

        return b;
    }

    public static boolean isDateValid(String date)
    {
        try {
            String DATE_FORMAT = "dd-MM-yyyy";
            DateFormat df = new SimpleDateFormat(DATE_FORMAT);
            df.setLenient(false);
            df.parse(date);
            return true;
        } catch (ParseException e) {
            return false;
        }
    }

    // TODO: Rename method, update argument and hook method into UI event
    public void onButtonPressed(Uri uri) {
        if (mListener != null) {
            mListener.onFragmentInteraction(uri);
        }
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        try {
            mListener = (OnFragmentInteractionListener) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString()
                    + " must implement OnFragmentInteractionListener");
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        public void onFragmentInteraction(Uri uri);
    }

}
