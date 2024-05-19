<template>
  <v-card color="grey-lighten-4" height="65px" rounded="0">
    <v-toolbar>
      <v-select
        label="Year From"
        style="margin-inline-start: 20px"
        :items="meteorsFilterData.years"
        v-model="meteorsFilter.yearFrom"
        hide-details="auto"
      ></v-select>
      <v-spacer></v-spacer>
      <v-select
        label="Year To"
        :items="meteorsFilterData.years"
        v-model="meteorsFilter.yearTo"
        hide-details="auto"
      ></v-select>
      <v-spacer></v-spacer>
      <v-select
        label="Class"
        style="width: 150px"
        :items="meteorsFilterData.recclasses"
        v-model="meteorsFilter.recclass"
        hide-details="auto"
      ></v-select>
      <v-spacer></v-spacer>
      <v-text-field
        label="Name"
        v-model="meteorsFilter.name"
        hide-details="auto"
      ></v-text-field>
      <v-spacer></v-spacer>
      <v-btn variant="outlined" @click="onGetMeteors()">Search</v-btn>
    </v-toolbar>
  </v-card>
  <v-data-table-server
    v-model:items-per-page="meteorsFilter.itemsPerPage"
    :headers="headers"
    :loading="loading"
    :items="meteors"
    :page="meteorsFilter.page"
    :items-length="meteorsTotal.rowsCount"
    @update:options="loadItems"
  >
    <template v-slot:tfoot>
      <tfoot>
        <tr>
          <td>
            <span class="text-subtitle-2">Total</span>
          </td>
          <td>
            <span class="text-subtitle-2">{{ meteorsTotal.count }}</span>
          </td>
          <td>
            <span class="text-subtitle-2">{{ meteorsTotal.mass }}</span>
          </td>
        </tr>
      </tfoot>
    </template>
  </v-data-table-server>
</template>

<script setup>
import axios from "axios";
import { onMounted, reactive, ref } from "vue";

const meteors = ref([]);
const meteorsTotal = reactive({ count: 0, mass: 0.0, rowsCount: 0 });
const meteorsFilterData = reactive({ recclasses: [], years: [] });
const meteorsFilter = reactive({
  yearFrom: 1,
  yearTo: new Date().getFullYear(),
  recclass: "All",
  name: "",
  sortAsc: true,
  sortField: "year",
  itemsPerPage: 5,
  page: 1,
});

const loading = ref(true);
const headers = [
  { title: "Year", align: "start", key: "year" },
  { title: "Meteors Count", align: "start", key: "count" },
  { title: "Total Mass", align: "start", key: "mass" },
];

function loadItems({ page, itemsPerPage, sortBy }) {
  meteorsFilter.page = page;
  meteorsFilter.itemsPerPage = itemsPerPage;
  if(sortBy.length > 0){
    meteorsFilter.sortField = sortBy[0].key;
    meteorsFilter.sortAsc = sortBy[0].order == "asc" ? true : false;
  }
  
  onGetMeteors();
}

function onGetMeteors() {
  debugger
  loading.value = true;
  Promise.all([
    axios.get("https://localhost:7182/meteor/meteors", {
      params: meteorsFilter,
    }),
    axios.get("https://localhost:7182/meteor/meteorsTotal", {
      params: meteorsFilter,
    }),
  ]).then(([meteorsRes, meteorsTotalRes]) => {
    meteors.value = meteorsRes.data;
    meteorsTotal.count = meteorsTotalRes.data.count;
    meteorsTotal.mass = meteorsTotalRes.data.mass;
    meteorsTotal.rowsCount = meteorsTotalRes.data.rowsCount;
    loading.value = false;
  });
}

onMounted(() => {
  axios.get("https://localhost:7182/meteor/filter").then(function (response) {
    meteorsFilterData.recclasses = response.data.recclasses;
    meteorsFilterData.recclasses.unshift(meteorsFilter.recclass);

    var minYear = response.data.minYear;
    var currentYear = new Date().getFullYear();
    while (minYear <= currentYear) {
      meteorsFilterData.years.push(minYear++);
    }
  });
});
</script>
