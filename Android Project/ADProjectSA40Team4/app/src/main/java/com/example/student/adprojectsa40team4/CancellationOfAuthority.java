package com.example.student.adprojectsa40team4;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.net.Uri;
import android.os.Bundle;
import android.app.Fragment;
import android.os.StrictMode;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import org.w3c.dom.Text;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Calendar;


/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link CancellationOfAuthority.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link CancellationOfAuthority#newInstance} factory method to
 * create an instance of this fragment.
 */
public class CancellationOfAuthority extends Fragment implements View.OnClickListener{
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    TextView vtE;
    Button btnSubmit;
    String DInfoID="";


    private OnFragmentInteractionListener mListener;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment CancellationOfAuthority.
     */
    // TODO: Rename and change types and number of parameters
    public static CancellationOfAuthority newInstance(String param1, String param2) {
        CancellationOfAuthority fragment = new CancellationOfAuthority();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    public CancellationOfAuthority() {
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
        View v = inflater.inflate(R.layout.fragment_cancellation_of_authority, container, false);

        vtE = (TextView) v.findViewById(R.id.tvE);

        StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);

        DelegatedInfoModel.CancellationOfAuthority ca = DelegatedInfoModel.GetDelegatedInfo(LogInModel.empDept.toString());
        String emp;

        if (!isNull(ca)) {
            emp = ca.employeeName.toString();
            vtE.setText(emp);
        }
        else
            Toast.makeText(getActivity().getApplicationContext(), "No delegated Person!", Toast.LENGTH_SHORT).show();

        btnSubmit = (Button) v.findViewById(R.id.btnSubmit);
        btnSubmit.setOnClickListener(this);
        return v;
    }

    private boolean isNull(Object obj) {
        return obj == null;
    }

    @Override
    public void onClick(View v) {
        if (v == btnSubmit) {
            if (vtE.getText() != "") {

                AlertDialog.Builder builder = new AlertDialog.Builder(v.getContext());
                builder
                        .setTitle("Delegate Authority")
                        .setMessage("Are you sure you want to cancel authority for this person?")
                        .setPositiveButton("ACCEPT", new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int which) {
                                StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);

                                String RMessage = "";
                                RMessage = DelegatedInfoModel.DeleteDelegatedInfo(DelegatedInfoModel.DInfoID);

                                Toast.makeText(getActivity().getApplicationContext(), RMessage, Toast.LENGTH_SHORT).show();
                                vtE.setText("");
//                            android.app.FragmentManager fragmentManager = getFragmentManager();
//                            ARRFragment fragment=new ARRFragment();
//
//                            fragmentManager.beginTransaction()
//                                    .replace(R.id.container, fragment)
//                                    .commit();
                            }
                        })
                        .setNegativeButton("DECLINE", null)                        //Do nothing on no
                        .show();
            }
            else{
                Toast.makeText(getActivity().getApplicationContext(), "No delegated Person!", Toast.LENGTH_SHORT).show();
            }
        }

        return;
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

    /**
     * This interface must be implemented by activities that contain this
     * fragment to allow an interaction in this fragment to be communicated
     * to the activity and potentially other fragments contained in that
     * activity.
     * <p>
     * See the Android Training lesson <a href=
     * "http://developer.android.com/training/basics/fragments/communicating.html"
     * >Communicating with Other Fragments</a> for more information.
     */
    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        public void onFragmentInteraction(Uri uri);
    }

}
