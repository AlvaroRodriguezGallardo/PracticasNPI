package com.example.prueba.ui.horario

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import com.example.prueba.databinding.FragmentHorarioBinding

class HorarioFragment : Fragment() {

    private var _binding: FragmentHorarioBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!

    override fun onCreateView(
            inflater: LayoutInflater,
            container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View {
        val horarioViewModel =
                ViewModelProvider(this).get(HorarioViewModel::class.java)

        _binding = FragmentHorarioBinding.inflate(inflater, container, false)
        val root: View = binding.root

        val textView: TextView = binding.textHorario
        horarioViewModel.text.observe(viewLifecycleOwner) {
            textView.text = it
        }
        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}