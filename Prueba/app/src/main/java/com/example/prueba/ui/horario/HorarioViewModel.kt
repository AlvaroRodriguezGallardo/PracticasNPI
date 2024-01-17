package com.example.prueba.ui.horario

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel

class HorarioViewModel : ViewModel() {

    private val _text = MutableLiveData<String>().apply {
        value = "This is horario Fragment"
    }
    val text: LiveData<String> = _text
}